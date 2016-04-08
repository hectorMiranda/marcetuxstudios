---
layout: post
title: "Kubernetes is on my radar and I am not ready for it"
date: 2016-04-07
author: marcetux
tags: [kubernetes, docker, devops, containers, architecture]
---
Kubernetes 1.2 shipped last week and every conference talk I've seen in the last six
months has had a K8s logo somewhere in it. At JibJab we're running Docker containers on
EC2 with an Auto Scaling Group, which is functional but not sophisticated. I spent a
weekend with Kubernetes on minikube to understand what the fuss is about, and I now
understand it — I'm just not sure we're at the complexity level where we need it.

The mental model is solid: a cluster of nodes managed by a control plane, workloads
described as declarative specs (Pods, Deployments, Services), the scheduler placing pods
on nodes according to resource requests. The killer feature over bare EC2 is that the
system continuously reconciles desired state with actual state — if a node dies, the
affected pods restart elsewhere without human intervention. We do that today with Auto
Scaling Group health checks and stateless workers; Kubernetes does it for any
workload, not just the ones you designed for it.

What I'm not ready to take on: the operational overhead of running the control plane. A
managed Kubernetes service would change the calculus, and there are hints Google will
eventually make GKE easier to adopt; AWS hasn't moved on ECS beyond the basics. When
managed K8s is a two-hour setup rather than a week of cluster configuration, the
trade-off tips. For now the Auto Scaling Group solution is good enough and cheap to
operate. Not every problem needs the best-practice solution today.
