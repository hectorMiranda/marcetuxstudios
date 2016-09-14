---
layout: post
title: "Kubernetes second look after 1.4"
date: 2016-09-13
author: marcetux
tags: [kubernetes, docker, devops, containers, architecture]
---
Kubernetes 1.4 shipped in September with `kubeadm` — a tool that sets up a Kubernetes
cluster in under fifteen minutes on clean machines. In April I said "when managed K8s
is a two-hour setup rather than a week of cluster configuration, the trade-off tips."
`kubeadm` is closer to that line than I expected. I spent a Saturday setting up a test
cluster on three EC2 instances and walked away with a functioning control plane.

The Kubernetes model hasn't changed from my April assessment, but my appreciation of
it has deepened. The thing I underweighted in April is the Deployment resource and
rolling updates: you describe the desired state (run 5 replicas of image v2), and the
control plane figures out how to get there from the current state (4 replicas of v1,
1 of v2) without you coordinating it. The rolling update is atomic across the fleet.
Our current Auto Scaling Group approach does something similar but requires more
manual coordination and has no built-in rollback.

My updated opinion: for teams larger than four or five engineers who are running more
than a handful of distinct services, Kubernetes is worth the operational investment.
For a small team with a small fleet, the Auto Scaling Group approach is cheaper to
operate and the marginal benefit is lower. The right point to cross the line is when
the coordination overhead of the simpler approach starts costing more than the
Kubernetes learning curve. We haven't crossed it yet. But the gap is narrowing.
