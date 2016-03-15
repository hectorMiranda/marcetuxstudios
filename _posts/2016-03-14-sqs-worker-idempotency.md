---
layout: post
title: "Making SQS workers idempotent"
date: 2016-03-14
author: marcetux
tags: [aws, sqs, queues, reliability, architecture]
---
SQS delivers messages at least once, which is almost once, except when it isn't.
Under normal conditions a message is delivered to one consumer and deleted after
acknowledgment. But under certain conditions — network partitions, worker crashes
during the visibility timeout, SQS internal retries — the same message can be
delivered twice. If your worker is not idempotent, you get duplicate work. For a
transcoding pipeline, duplicate work means duplicate billing and duplicate S3
objects. Neither is the end of the world; a billing double-dip that happens 0.1%
of the time is not a crisis. But it also doesn't have to happen.

The fix is an idempotency key: before doing the expensive work, check whether a record
with this job ID already exists in a state table. If it does, delete the message and
return. If it doesn't, write a "processing" record, do the work, update to "complete."
The at-least-once delivery contract becomes at-most-once real work because the
deduplication happens in your own data store under your own consistency rules.

The state table is a small DynamoDB table keyed on job ID with a TTL — items expire
after 24 hours, longer than any plausible re-delivery window. DynamoDB conditional
writes let me assert "this item doesn't exist yet" atomically, which handles the
race between two workers picking up the same message simultaneously. It's a little
extra infrastructure but idempotent workers sleep better. Or I do.
