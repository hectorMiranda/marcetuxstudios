---
layout: post
title: "Load testing before you need to"
date: 2019-11-04
author: marcetux
tags: [testing, performance, architecture, devops, banking]
---
The payment service went to production in March without a load test. I knew it at the time and filed it away as a debt item. November was when that debt came due — not in production, thankfully, but in a load test we finally ran and found a problem we should have found eight months earlier. The connection pool on the SQL client was sized for an expected concurrent request count that the actual load exceeded by a factor of three during peak hours.

The discovery process with NBomber: define a scenario — in this case, the payment initiation endpoint at realistic request rates — and run it against a staging environment that mirrors production sizing. NBomber reports request rate, latency percentiles, and error rates. The p99 latency was fine at 500 requests per minute; at 2000 it climbed and at 2500 the error rate started rising. The Dynatrace trace waterfall for the failing requests showed the database connection wait as the bottleneck — requests were queuing waiting for a pool connection rather than executing.

The fix was resizing the pool and tuning the timeout, but the more important output is the load profile itself. We now know the service's breaking point. Capacity planning has a number to work with. The on-call runbook has a section on "if payment latency spikes during peak load, check this metric first." Finding the breaking point in a scheduled load test against staging is categorically different from finding it in production at 5 PM on a Friday. The load test is cheap; the production incident is not.
