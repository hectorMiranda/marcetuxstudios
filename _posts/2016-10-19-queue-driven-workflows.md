---
layout: post
title: "Queue-driven workflows and keeping state out of the queue"
date: 2016-10-19
author: marcetux
tags: [sqs, queues, architecture, reliability, backend]
---
The video processing workflow at JibJab is now several steps: upload acknowledgment,
metadata extraction, thumbnail generation, transcoding for each rendition, delivery
registration, notification. Each step is a separate worker. Each worker listens to a
separate SQS queue. The outputs of one step trigger the inputs of the next. It works
well and has one antipattern I want to document because I see it in other systems.

The antipattern: putting job state in the message payload and relying on downstream
workers to carry it forward. A message for the transcoding step that includes the full
metadata payload, the thumbnail URLs, and the original upload metadata is convenient
for the worker but fragile for the system. If the message is re-delivered, the worker
gets the original snapshot of state, not the current one. If an upstream step adds a
field, downstream messages need to be updated too. The message becomes a denormalized
copy of state that drifts.

The correct model: the message carries a job ID and the minimum data the worker needs
to identify the work. The worker fetches current state from the authoritative store —
DynamoDB in our case — performs its step, and writes back. The queue is coordination,
not storage. The job record in DynamoDB is the source of truth; the message is the
nudge. This makes re-delivery safe (the worker fetches current state regardless of
when the message was generated) and makes each step independent of what the other steps
put in their messages.

*Update: the job state record in DynamoDB uses a simple schema — `job_id` (partition key), `status` (enum: queued/processing/done/failed), `updated_at`, and per-step metadata fields like `thumbnail_s3_key` and `rendition_urls` (map). Workers update only their own fields with a conditional expression that checks `status = 'queued'` before claiming a job. The atomic claim is the idempotency gate from the earlier SQS post, applied to the state record itself.*
