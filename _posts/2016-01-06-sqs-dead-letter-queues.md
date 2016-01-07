---
layout: post
title: "SQS dead-letter queues and why I should have used them sooner"
date: 2016-01-06
author: marcetux
tags: [aws, sqs, queues, reliability, media]
---
Our video transcoding pipeline at JibJab is queue-driven: an upload triggers an SQS
message, a worker picks it up, produces the renditions, updates the database. It works
well until it doesn't — a message with a corrupt payload, a worker bug, a dependency
outage — and then the same bad message gets picked up, fails, goes back to the queue,
gets picked up again. Before I wired in dead-letter queues, bad actors stayed in the
pool and hammered workers for hours before anyone noticed.

The fix is one configuration field: set `maxReceiveCount` on the source queue and
point a dead-letter queue at it. After N visibility-timeout cycles without a deletion,
SQS moves the message to the DLQ automatically. The source queue drains normally;
poison messages end up somewhere they can be inspected, replayed one at a time, or
deleted deliberately. The DLQ becomes the on-call alert: if it's non-empty, something
is wrong and you have a copy of the evidence.

What surprised me was how fast debugging got easier. Instead of reconstructing the
failure from CloudWatch logs after the fact, the bad message is right there in the DLQ,
payload intact, ready to be tested against a fixed worker. Every queue-driven pipeline
should have one from day one. I just wish I'd added it day one.
