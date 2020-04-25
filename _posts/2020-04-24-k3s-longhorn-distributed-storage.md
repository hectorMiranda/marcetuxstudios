---
layout: post
title: "Longhorn on k3s and finally fixing the storage single point of failure"
date: 2020-04-24
author: marcetux
tags: [kubernetes, homelab, raspberry-pi, storage]
---
The InfluxDB node-pin that I called out in February as a gap finally got fixed this
weekend. Longhorn — Rancher's lightweight distributed block storage for Kubernetes —
supports ARM64 and installs on k3s without significant drama. The result is
replicated persistent volumes that can reschedule to any node when one goes down.

The install is a `kubectl apply` of the Longhorn manifest, a wait for the DaemonSets
to come up on each node, and then PersistentVolumeClaims that reference the Longhorn
storage class. I migrated InfluxDB by scaling the deployment to zero, backing up
the data directory to an external drive, creating a new PVC on Longhorn, restoring
into it, and scaling back up. Not seamless, but a one-time migration cost.

The replicas are set to two out of three nodes for the data volume. One node can be
offline — for an update, a crash, a power-cycle — and the data is intact on the
other two. Grafana has shown two brief scheduling events since the migration and
both resolved without me touching anything. The home lab now has better storage
resilience than the production system had before I fixed that, which is a mildly
embarrassing observation.
