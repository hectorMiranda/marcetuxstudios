#!/bin/bash
# Wire a source SQS queue to a dead-letter queue.
# Run once after both queues exist; maxReceiveCount = 5 means 5 failed receives
# before the message is moved to the DLQ.

REGION="us-east-1"
SRC_QUEUE="transcode-jobs"
DLQ_NAME="transcode-jobs-dlq"

# Get the ARN of the dead-letter queue
DLQ_ARN=$(aws sqs get-queue-attributes \
  --region "$REGION" \
  --queue-url "https://sqs.${REGION}.amazonaws.com/$(aws sts get-caller-identity --query Account --output text)/${DLQ_NAME}" \
  --attribute-names QueueArn \
  --query Attributes.QueueArn --output text)

# Apply the redrive policy to the source queue
aws sqs set-queue-attributes \
  --region "$REGION" \
  --queue-url "https://sqs.${REGION}.amazonaws.com/$(aws sts get-caller-identity --query Account --output text)/${SRC_QUEUE}" \
  --attributes "{\"RedrivePolicy\":\"{\\\"deadLetterTargetArn\\\":\\\"${DLQ_ARN}\\\",\\\"maxReceiveCount\\\":\\\"5\\\"}\"}"

echo "DLQ wired: ${DLQ_ARN}"
