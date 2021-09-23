---
layout: post
title: "Graceful shutdown in ASP.NET Core and why it matters"
date: 2021-09-22
author: marcetux
tags: [dotnet, aspnet, kubernetes, reliability]
---
Kubernetes rolling deployments had been causing a small but consistent number of
502 errors during deploys. The cause was the sequence: Kubernetes sends SIGTERM to
the old pod, immediately stops routing new traffic to it, and expects the pod to
drain in-flight requests and exit. Our pod was exiting within milliseconds of
receiving SIGTERM — before the in-flight requests had a chance to complete. The
load balancer hadn't fully drained the connection pool yet, so some requests went
to a pod that was already shutting down.

ASP.NET Core handles SIGTERM through `IHostApplicationLifetime.ApplicationStopping`,
which triggers a graceful shutdown period. The `ShutdownTimeout` in the host options
defaults to five seconds. That's the window for in-flight requests to complete. The
problem was we'd overridden it to zero in an early optimization that made sense in
development and was wrong in production. Zero grace period means kill immediately.

The fix: set `ShutdownTimeout` to a value longer than our p99 request latency, and
add a pre-stop hook in the Kubernetes pod spec that sleeps two seconds before the
SIGTERM is sent. The sleep gives the load balancer time to drain connections before
the application gets the shutdown signal. Graceful shutdown is a distributed systems
problem that touches the load balancer, the orchestrator, and the application, and
you have to configure all three to agree on the sequence.
