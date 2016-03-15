"""
Idempotent SQS worker using DynamoDB for deduplication.
Before processing a message, it checks/creates a record in a DynamoDB table.
Duplicate deliveries (SQS at-least-once) are caught and discarded cheaply.
"""
import boto3
import json
import logging
from botocore.exceptions import ClientError

log = logging.getLogger(__name__)

sqs = boto3.client('sqs', region_name='us-east-1')
dynamo = boto3.resource('dynamodb', region_name='us-east-1')
dedup_table = dynamo.Table('transcode-jobs-dedup')

QUEUE_URL = 'https://sqs.us-east-1.amazonaws.com/ACCOUNT_ID/transcode-jobs'
TTL_SECONDS = 86400  # 24 hours


def claim_job(job_id: str, expiry: int) -> bool:
    """Returns True if this worker is the first to claim the job."""
    try:
        dedup_table.put_item(
            Item={'job_id': job_id, 'status': 'processing', 'ttl': expiry},
            ConditionExpression='attribute_not_exists(job_id)'
        )
        return True
    except ClientError as e:
        if e.response['Error']['Code'] == 'ConditionalCheckFailedException':
            return False
        raise


def process_message(body: dict):
    """The actual transcoding work goes here."""
    log.info("Transcoding job %s", body['job_id'])
    # ... invoke FFMPEG, upload to S3, etc.


def run():
    import time
    while True:
        resp = sqs.receive_message(QueueUrl=QUEUE_URL, MaxNumberOfMessages=1,
                                   WaitTimeSeconds=20)
        for msg in resp.get('Messages', []):
            body = json.loads(msg['Body'])
            job_id = body['job_id']
            expiry = int(time.time()) + TTL_SECONDS

            if not claim_job(job_id, expiry):
                log.info("Duplicate job %s — discarding", job_id)
            else:
                process_message(body)
                dedup_table.update_item(
                    Key={'job_id': job_id},
                    UpdateExpression='SET #s = :done',
                    ExpressionAttributeNames={'#s': 'status'},
                    ExpressionAttributeValues={':done': 'complete'}
                )

            sqs.delete_message(QueueUrl=QUEUE_URL,
                               ReceiptHandle=msg['ReceiptHandle'])
