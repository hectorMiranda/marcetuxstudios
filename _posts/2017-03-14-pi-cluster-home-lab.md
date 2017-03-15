---
layout: post
title: "Building a Pi cluster for the home lab"
date: 2017-03-14
author: marcetux
tags: [raspberrypi, homelab, docker, networking]
---
Running Kubernetes on AWS at work gave me a new itch: run it at home, on hardware I can
touch, for the cost of a few Raspberry Pi 3s. This is not a production workload
recommendation — it's the kind of project where the point is building and breaking it
yourself rather than having an opinion about whether it's efficient. Three Pi 3Bs arrived
this week and I have a weekend and a switch.

The setup: one Pi as the master node, two as workers. Static IPs assigned on the router,
SSH keys deployed, hostnames set. I'm running Docker on HypriotOS, which saves the
manual Docker-on-ARM setup and gets straight to the interesting part. Kubernetes on ARM
involves a bit more patience — a few images don't have ARM builds in public registries
and need to be compiled, and the resource constraints are real. The cluster has 4GB of
RAM total, which is enough to run a handful of services but not the full monitoring
stack I'd eventually like.

The thing the home cluster is good for that you can't replicate in a cloud trial: you
experience the network — the latency between nodes, the behavior of a pod on one Pi
reaching a service on another, what actually happens to in-flight requests when you
unplug a node. I pulled the ethernet cable on worker-2 while a small web app was
running. Kubernetes moved the pods to worker-1 in about 45 seconds. That's the loop
that builds the intuition.
