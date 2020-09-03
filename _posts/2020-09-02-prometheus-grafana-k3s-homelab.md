---
layout: post
title: "Prometheus and Grafana on the home k3s cluster"
date: 2020-09-02
author: marcetux
tags: [kubernetes, prometheus, grafana, homelab]
---
Running a Kubernetes cluster and watching it with `kubectl top nodes` is like
driving with the speedometer taped over — you know roughly how fast you're going but
nothing about the trend. September's first lab task: proper metrics with Prometheus
and Grafana on the k3s cluster. The kube-prometheus-stack Helm chart gets you
both, plus AlertManager and a set of default dashboards for cluster and node
metrics, in one install.

The chart is not small — it deploys around fifteen components — and on three Pi 4s
with 4 GB RAM each, I had to tune the Prometheus retention window (15 days, not 15 weeks)
and the scrape interval (30 seconds, not 15) to keep the memory footprint manageable.
After that the stack runs happily. The default dashboards show CPU, memory, and
network per node and per namespace; I added a custom dashboard that overlays the
sensor-stack metrics (MQTT message rate, InfluxDB query latency) alongside the
infrastructure metrics.

The thing that surprised me: seeing the k3s cluster's own resource usage graphed over
time revealed that the Longhorn replication traffic has a predictable pattern — heavy
on the hour when a node syncs a volume replica. I'd have never noticed that with
`kubectl top`. Visibility into behavior you didn't know existed is the argument for
observability, and it's easier to make when you're looking at it.
