---
layout: post
title: "CloudWatch custom metrics for pipeline health"
date: 2016-08-24
author: marcetux
tags: [aws, cloudwatch, monitoring, devops, metrics]
---
The default CloudWatch metrics for EC2 and SQS tell you whether the infrastructure is
running, not whether the application is healthy. Queue depth going up is an
infrastructure signal; "jobs are completing faster than they arrive" is an application
signal, and only one of them tells you whether the product is working. I spent a
week wiring application-level metrics into CloudWatch's custom metrics API.

The API is straightforward: `PUT MetricData` with a namespace, a metric name, a value,
and optional dimensions. Workers now emit after every job completion: transcoding
duration in seconds (by rendition and resolution), bytes processed, and a success/
failure counter. The namespace is `JibJab/Transcoding` and the dimensions let me slice
by rendition type. CloudWatch aggregates across the fleet automatically.

What this unlocked: an alarm on the `p95 transcoding duration` metric that fires when
the 95th percentile transcode time exceeds twice the baseline. Slow transcodes are
usually a symptom of a worker hitting a resource constraint, not a queue problem. The
alarm fires before the queue depth metric does, giving more lead time. Infrastructure
alarms tell you something broke; application alarms tell you something is degrading.
Both are necessary; only one of them catches problems while they're still recoverable.
