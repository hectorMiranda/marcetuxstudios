---
layout: post
title: "Kubernetes is enough for a small team now"
date: 2018-01-06
author: marcetux
tags: [kubernetes, devops, docker, infrastructure, containers]
---
The question I was still asking last year — "is Kubernetes overkill for a five-person
startup?" — got settled by actually running it. We'd been on ECS for the first Go RN
services, which works fine until you need to express things ECS simply doesn't have:
rolling updates with a real readiness gate, pod disruption budgets, a single place to
look at all the logs from a given deploy. GKE gave us a cluster in an afternoon.

The thing that clicked was that Kubernetes shifts the contract. Instead of "deploy this
Docker image to these instances," you declare what the system should look like and
reconciliation is the cluster's problem. A node goes away? The scheduler moves the
pods. You push a bad image? The readiness probe fails and the rollout stops before it
kills production. The control loop handles the boring disaster recovery that used to
be a Friday runbook.

The gotcha is the YAML. There is a lot of YAML, and it multiplies fast, and nothing
tells you when you've written it wrong until a pod is in CrashLoopBackOff at midnight.
Start with Helm early, or at least share partial templates across services before the
copy-paste inertia sets in. The underlying model is right; the surface area takes a
few burns to navigate.
