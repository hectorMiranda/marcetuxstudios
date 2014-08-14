---
layout: post
title: "SQS and background job decoupling at Spark"
date: 2014-08-13
author: marcetux
tags: [aws, sqs, ruby, rails, background-jobs, queues]
---
The photo processing pipeline at Spark runs through SQS. A browser uploads the photo
to S3, the Rails app confirms the upload and enqueues an SQS message, and a separate
worker process picks up the message and runs the crop, resize, and thumbnail generation.
The app server response time is independent of how long photo processing takes — the
user gets a response immediately and the processing happens whenever a worker gets to it.

SQS messages have a visibility timeout: once a worker takes a message, the message
disappears from the queue for that window. If the worker finishes and deletes the message,
that's the happy path. If the worker crashes or takes too long, the visibility timeout
expires and the message reappears in the queue for another worker to pick up. Idempotent
workers — ones that can process the same message twice without causing problems — are
therefore essential. Writing a thumbnail twice is fine; charging a credit card twice is not.

The quirk of SQS compared to Redis-backed queues like Sidekiq is at-least-once delivery:
SQS guarantees a message will be delivered but doesn't guarantee it'll be delivered
exactly once. In practice duplicates are rare, but designing for them upfront is easier
than adding idempotency checks after you've seen your first duplicate invoice. The
photo worker checks whether the thumbnail already exists before generating it. Cheap and
safe.
