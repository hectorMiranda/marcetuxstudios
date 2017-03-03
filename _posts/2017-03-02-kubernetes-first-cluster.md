---
layout: post
title: "Standing up a Kubernetes cluster and changing how I think about deploys"
date: 2017-03-02
author: marcetux
tags: [kubernetes, docker, devops, architecture]
---
I've been running Docker containers in production for over a year, but always managed
them by hand: SSH into a host, pull the image, restart the container. It works until it
doesn't — until you need to deploy across three hosts without downtime, or restart a
crashed container automatically, or drain a node for maintenance without dropping
requests. That's the gap Kubernetes fills, and I finally stood up a small cluster on
AWS this month to understand it at a hands-on level rather than a conference-slide one.

The core concepts that clicked: a **Pod** is the unit of scheduling, not a container —
it's one or more containers that share a network namespace and always land on the same
node. A **Deployment** describes the desired state ("I want three replicas of this pod
running"), and Kubernetes continuously reconciles actual state against that. A
**Service** gives the pod set a stable IP and DNS name so the rest of the cluster can
find it regardless of which nodes the pods land on. Three concepts, and most of what
you do day to day maps onto them.

The thing that didn't click until I broke it: `kubectl apply -f` is not "run this."
It's "make the cluster look like this." If I reduce replicas, pods are terminated. If I
change the image tag, pods are rolled. The declarative model requires you to think about
the *desired end state*, not the imperative steps to get there. That's a different
muscle, and worth building early before the cluster gets complicated.
