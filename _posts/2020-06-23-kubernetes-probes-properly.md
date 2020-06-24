---
layout: post
title: "Kubernetes probes and getting them right the second time"
date: 2020-06-23
author: marcetux
tags: [kubernetes, dotnet, devops, reliability]
---
The AKS upgrade surfaced a service that was getting liveness-probe killed under
load. The symptom: brief 502s during high-traffic periods, pods restarting in the
event log, no application crash recorded. The cause: a liveness probe hitting a
health endpoint that called the database, and under load the database call slowed
past the probe's timeout threshold — so Kubernetes killed what it thought was an
unhealthy pod that was actually just busy.

The correct separation is liveness versus readiness. **Liveness** answers "should
this pod be killed and restarted?" — it should only check things that indicate the
process is stuck or deadlocked: a thread-stall detector, an in-memory circuit
breaker, something that's meaningfully wrong regardless of load. **Readiness**
answers "should traffic be routed to this pod?" — this can check dependencies,
database connectivity, downstream health. If readiness fails under load, the pod is
removed from the load balancer pool without being killed. It recovers when the load
passes. Liveness killing should be the last resort.

We split the health endpoint into `/health/live` and `/health/ready`, configured
appropriately, and the 502s during high traffic stopped. The pods that were fine
but overwhelmed now shed load gracefully instead of being executed for being busy.
Probe semantics matter; they're not interchangeable.
