---
layout: post
title: "Pi cluster back to basics with k3s"
date: 2024-10-15
author: marcetux
tags: [homelab, kubernetes, k3s, raspberry-pi, infrastructure]
---
I rebuilt the Pi cluster this month — four Pi 5s, k3s instead of the full k8s I was
running, and a cleaner networking setup than the last iteration. The reason for the
rebuild: I'd accumulated enough cruft in the old setup that debugging a failing pod
required remembering decisions I'd made two years ago and never wrote down. Starting
fresh with documented intent is faster than archaeological debugging.

k3s earns its place on Pi hardware. Full Kubernetes has an orchestrator footprint
that competes meaningfully with a Pi 4 or 5's memory budget once you add a few
workloads. k3s is a CNCF-certified Kubernetes distribution that strips the in-tree
cloud plugins and replaces etcd with SQLite for small clusters. The API surface is
the same; the resource consumption is about 300 MB instead of 600+ for the control
plane. On a Pi with 8 GB that still leaves room; on a Pi with 4 GB it's the
difference between comfortable and constantly under pressure.

The thing I added this time that I hadn't before: Longhorn for persistent volume
claims across nodes. Previously I was mounting host paths, which is fine until you
need to move a pod to a different node and discover the data is on the old one. Longhorn
replicates volumes across nodes over the network. It adds I/O latency — NFS would be
faster — but it's Kubernetes-native, works with PVCs, and the replication means a
single Pi failure doesn't lose data. The cluster is now a real environment for testing
deployment patterns, not just a demo.
