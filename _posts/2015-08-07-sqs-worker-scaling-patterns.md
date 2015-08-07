---
layout: post
title: "SQS worker scaling patterns at JibJab"
date: 2015-08-07
author: marcetux
tags: [aws, sqs, scaling, video, devops]
---
The render queue at JibJab is not flat: holiday traffic spikes orders of magnitude above
the baseline, and a fixed worker pool sized for peak is expensive to run year-round.
The scaling pattern we have is queue-depth-driven: CloudWatch watches the SQS
`ApproximateNumberOfMessagesVisible` metric and triggers an Auto Scaling policy when the
queue backs up past a threshold.

The policy is a step scaling rule: queue depth 0–50, one worker; 50–200, four workers;
200+, eight workers. The scale-out is fast — new EC2 instances spin up in a few minutes
with the worker AMI baked in, no configuration step at boot. Scale-in is slower by
design: we wait 15 minutes before terminating a worker to avoid thrash during a burst
that might resume. A render job that dies mid-process because the worker was terminated
requires a retry and costs more than the instance savings.

The one failure mode we've been working around: when the queue drains faster than
CloudWatch's 1-minute polling interval updates the metric, you can scale to eight
workers and have two jobs left. The workers idle, the scale-in timer starts, the workers
terminate after 15 minutes. This is fine — the over-provisioning window is bounded —
but it means the Auto Scaling approach works well for sustained load and is a little
wasteful for sharp spikes. For JibJab's traffic shape, the sustained holiday load is the
real problem, and queue-depth scaling handles it correctly.
