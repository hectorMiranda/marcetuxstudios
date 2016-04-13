---
layout: post
title: "Lambda as a glue layer in event-driven pipelines"
date: 2016-04-12
author: marcetux
tags: [aws, lambda, serverless, architecture, pipeline]
---
Lambda has been around since 2014 and I've been treating it as a curiosity until this
month, when I needed a small piece of work to happen on an S3 event and didn't want to
build a service for it. An S3 `ObjectCreated` event triggers a Lambda that inspects
the metadata, writes a record to DynamoDB, and fires an SQS message into the transcoding
queue. Forty lines of Python, no servers, billed per invocation.

The event-driven model is what makes it genuinely useful rather than just novelty. The
S3 bucket doesn't know anything about the transcoding pipeline; the Lambda is the
connection. If the pipeline changes — different queue, additional notification, audit
log — I update the Lambda without touching the bucket configuration or the workers. The
Lambda is boundary code, and boundary code is the right thing to make small and
replaceable.

The things that surprised me: cold start latency is real but rarely matters for async
pipelines (a 500ms first-invocation delay on a job that takes 30 seconds to process
is not a problem). The execution environment is constrained in ways that occasionally
bite — disk is limited, the Lambda role needs exactly the permissions you forgot to
add — but the debugging experience via CloudWatch Logs is better than I expected. It
is the right tool for this specific job: event-driven glue that runs infrequently and
has no reason to keep state.
