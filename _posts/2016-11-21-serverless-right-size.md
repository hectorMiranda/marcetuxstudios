---
layout: post
title: "Right-sizing Lambda and knowing when not to use it"
date: 2016-11-21
author: marcetux
tags: [aws, lambda, serverless, architecture]
---
I've been using Lambda for S3 event processing since April and the pattern has held
up. But finishing out this job means cataloging honestly which problems Lambda is right
for and which ones I would have been better off solving differently. The gap between
"Lambda can do this" and "Lambda is the right tool for this" is larger than the
evangelists admit.

Where Lambda earns its keep: event-driven glue between services that don't know about
each other. S3 object created, DynamoDB stream changed, SQS message batch received as
a trigger for a small amount of work. Functions that run infrequently, have no warm-path
requirements, and have clear invocation boundaries. The economics are compelling for
these cases — you pay per invocation and the infrastructure is zero.

Where Lambda becomes trouble: anything with a consistent high request rate where cold
starts affect latency, anything that needs a long-running connection (websockets, streaming),
anything that needs more than 512 MB of memory or more than 5 minutes of execution
time, and anything where the local development and testing story matters. Testing a
Lambda locally is possible but the gap between local and production behavior is wider
than for a regular application. Lambda is exactly right for the event-driven glue jobs.
It is the wrong answer for the things it technically can do but wasn't designed for.
