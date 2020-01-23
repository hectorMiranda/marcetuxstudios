---
layout: post
title: "Standing up k3s on the Pi cluster"
date: 2020-01-22
author: marcetux
tags: [kubernetes, raspberry-pi, homelab, k3s]
---
The three Pi 4s I ordered in December have been sitting in a drawer waiting for the
right project. January's project: turn them into a lightweight Kubernetes cluster
using k3s — Rancher's stripped-down distribution that actually runs on ARM without
heroics. This is partly curiosity, partly wanting a home lab that matches the stack
at work close enough to be useful.

k3s replaces the full control-plane components with lighter binaries and bakes in a
SQLite backend instead of etcd, which is plenty for a three-node home lab. The
install is one curl-piped-to-bash on the server node and a single join command on each
agent with the node token. Ten minutes and the cluster was running. I deployed
a small Node app with a LoadBalancer service backed by MetalLB for a real IP on the
home network. Watching `kubectl get pods` come up on actual hardware I own has a
satisfying concreteness that cloud-only clusters don't.

The goal for the next few weeks: deploy the home sensor stack onto it — the MQTT
broker, the Node-RED flows, the InfluxDB instance that holds the temperature and
energy data. It's running on a single Pi right now, which is one power-cycle away
from losing days of data. Spreading it across three nodes and letting k3s manage the
restarts is boring and right.
