---
layout: post
title: "Health check endpoints are not optional"
date: 2018-02-07
author: marcetux
tags: [kubernetes, devops, aspnet, architecture, reliability]
---
A pod was quietly failing to connect to the database after a misconfigured secret
rotation, returning HTTP 500 on every data request while reporting itself as alive.
Kubernetes kept routing traffic to it because the liveness probe was a simple TCP
knock on port 80, and port 80 was open — the app just couldn't do anything useful.
Forty minutes of confused on-call before someone checked pod logs.

The distinction that mattered: a **liveness** probe should answer "should Kubernetes
restart me?" — a failing dependency doesn't warrant a restart if the dependency will
recover. A **readiness** probe answers "should Kubernetes send me traffic?" — and a
pod that can't reach its database absolutely should not be receiving requests. Once
I separated the two and wired the readiness check to an actual DB ping and a cache
probe, the scheduler figured it out automatically on the next bad deploy.

In ASP.NET Core 2.0 this is a few lines: register the health checks package, add
your custom checks, expose the endpoints, configure your probe paths. The framework
does the plumbing; you write the thing-that-actually-matters logic. The real discipline
is deciding what "ready" means for your service before an incident teaches you. I'd
rather write the spec at 2 PM on a Tuesday than at 2 AM on a Saturday.
