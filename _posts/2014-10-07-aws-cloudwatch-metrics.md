---
layout: post
title: "CloudWatch custom metrics for application health"
date: 2014-10-07
author: marcetux
tags: [aws, cloudwatch, monitoring, devops, observability]
---
The infrastructure alerts at Spark were CPU and memory on the EC2 instances, which is
fine for "is the machine alive" and useless for "is the application healthy." A machine
can be alive and perfectly responsive while the application is silently failing to send
emails, processing SQS messages half as fast as they arrive, or serving stale Couchbase
data because a bucket eviction spike emptied the cache. The machine-level view doesn't
see application-level problems.

CloudWatch custom metrics let you push any number — with a name, dimensions, and a
timestamp — into CloudWatch from application code or a cron script. The SQS worker now
emits a `job.processed` metric after each successful job and a `job.failed` metric on
failure. A CloudWatch alarm on `job.failed` rate fires a PagerDuty alert. The email
delivery service emits `email.queued` and `email.delivered` with a lag metric between
them; if lag exceeds thirty minutes something is wrong with the worker, not the machine.

The custom metric is about three lines of code using the AWS SDK. The value it creates
is alerts that care about what users experience, not what the server is doing.
Machine metrics are necessary; they're not sufficient. The question "is the application
healthy" requires application data to answer, and CloudWatch is the place that data
lives.
