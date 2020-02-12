---
layout: post
title: "Moving the home sensor stack onto k3s"
date: 2020-02-11
author: marcetux
tags: [kubernetes, raspberry-pi, homelab, iot]
---
The home sensor stack has been running on a single Pi 4 since last summer —
Mosquitto, Node-RED, InfluxDB, Grafana. It works until it doesn't, and "doesn't"
usually means a filesystem corruption after an unclean shutdown. Moving it to the
three-node k3s cluster was the February lab goal.

The migration surfaced the thing about containerizing stateful workloads on bare
metal: persistent volumes. k3s ships with local-path provisioner out of the box, which
is fine for development but means your data lives on exactly one node. I mounted the
InfluxDB volume with a `nodeSelector` pinning it to the node with the fastest SD card,
accepted that as a single point of failure for now, and kept going. The right answer
is a distributed storage layer — Longhorn looks promising — but scope creep is real
and the point was getting the stack running, not building the perfect cluster.

The payoff: Mosquitto crashed twice last week and k3s restarted it in under five
seconds each time. The Grafana dashboard shows a flat gap where the broker was dark
and then data resumes. On a single Pi that gap would have been "go look at the logs
and restart it manually." Boring resilience is the best kind.
