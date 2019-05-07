---
layout: post
title: "ASP.NET Core health checks done properly"
date: 2019-05-06
author: marcetux
tags: [dotnet, aspnetcore, kubernetes, observability, devops]
---
Kubernetes liveness and readiness probes need somewhere to call. Before we had proper health check endpoints, every service had a `/ping` route that returned 200 regardless of whether the database was up, the bus was reachable, or the downstream dependencies were responding. Kubernetes would happily keep routing traffic to a pod whose database connection pool had exhausted — because `/ping` always said "I'm fine."

ASP.NET Core's health check middleware is the right fix. You register checks in startup — `AddSqlServer`, `AddAzureServiceBusTopic`, custom checks for anything else — and the framework aggregates them into a `Healthy`, `Degraded`, or `Unhealthy` response. The Kubernetes readiness probe hits `/health/ready`; if the database is unreachable that pod drops out of the load balancer rotation while the liveness probe (`/health/live`) keeps the pod from being unnecessarily restarted. Ready is "can I serve traffic right now"; live is "is the process itself functional." They're different questions and they need different endpoints.

The detail that matters for a banking system: the readiness check should not call payment processors or ledger services that we don't own. If a third-party payment network has an outage, you don't want your pod to go unhealthy and take your own service down with it. We check only the dependencies we're responsible for. The payment network dependency gets its own circuit-breaker metric monitored separately. Health checks should reflect your own health, not cascade someone else's problems into your orchestration layer.
