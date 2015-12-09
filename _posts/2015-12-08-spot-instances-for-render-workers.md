---
layout: post
title: "EC2 Spot instances for the render worker fleet"
date: 2015-12-08
author: marcetux
tags: [aws, ec2, spot-instances, cost, devops]
---
The render workers are the right workload for EC2 Spot instances: they're stateless,
they process jobs from a queue, and a terminated instance just returns the job to SQS
for another worker to pick up. The Spot price for the compute-optimized instance type
we use runs about 30% of the On-Demand price during off-peak and 70% during holiday
peak — still cheaper than On-Demand, and the interruption risk is manageable because
of how the queue handles it.

The interruption handling is the piece that needs explicit implementation. When AWS is
about to reclaim a Spot instance it sends a two-minute warning via the instance metadata
endpoint. The worker polls that endpoint every minute — if the termination notice is
present, the worker stops accepting new jobs, finishes the current one or abandons it
by making the SQS message visible again, and shuts down gracefully. The job that was in
flight returns to the queue; another worker picks it up. No job is lost, just delayed.

The practical configuration: Spot instances in the Auto Scaling group with On-Demand as
a fallback if the Spot price exceeds our bid. We've run a week at peak holiday and the
Spot pool hasn't been interrupted — the Spot price hasn't exceeded bid. Even if it does,
the on-demand fallback maintains capacity. The cost is lower, the resilience is the
same, and the code to handle termination is eighty lines of polling and graceful exit.
Worth it.
