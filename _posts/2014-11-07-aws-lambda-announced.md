---
layout: post
title: "AWS Lambda announced and the serverless idea"
date: 2014-11-07
author: marcetux
tags: [aws, lambda, serverless, cloud, architecture]
---
AWS announced Lambda at re:Invent this week — functions that run in response to events
without provisioning servers. You upload code, configure the event trigger, and AWS
runs your code when the event fires. No EC2 to manage, no load balancer to configure,
no auto-scaling group to tune. The pitch is pure operational simplicity.

The trigger model is the interesting part. Lambda functions can be invoked by S3 events
(a new object was uploaded), SNS notifications, API Gateway requests, or directly.
For the S3 upload workflow I built at Spark — photo uploaded, SQS message, worker
process, thumbnail generated — a Lambda function triggered on S3 object creation would
eliminate the SQS step and the worker fleet. The function runs, does the thumbnail, and
exits. AWS manages the scaling from zero to thousands of concurrent invocations.

The skepticism I have is on long-running work and cold starts. Lambda functions have
execution time limits, which makes long thumbnail jobs for large photos a problem.
Cold starts — the latency hit when AWS spins up a new function instance — affect
latency-sensitive workloads. For asynchronous work triggered by events, those constraints
don't matter much. For synchronous API handlers, they do. The use case seems clear:
event-driven glue code and short-lived transformation work. Worth adding to the toolkit
when the situation fits.
