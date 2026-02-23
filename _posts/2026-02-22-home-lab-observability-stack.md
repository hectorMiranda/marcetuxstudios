---
layout: post
title: "The home lab observability stack"
date: 2026-02-22
author: marcetux
tags: [home-lab, observability, opentelemetry, linux, electronics]
---
Part of setting up the studio properly was deciding what I actually want the home lab to tell me about itself. The k3s cluster on the three nodes is running a handful of services — local DNS, some personal tooling, a Grafana+Tempo stack for the OTel experiments I do for work — and for a while I was just eyeballing the Grafana dashboards when something felt slow. That's fine until you want to know whether a slowness you noticed at 2 AM last Tuesday was a spike or a trend, and then you wish you'd been collecting the signal.

The stack I landed on is deliberately minimal: Prometheus scraping the node exporters and k3s metrics, Loki collecting logs from the pods, Tempo receiving traces from anything I'm developing that I want to test against real infrastructure before it goes to a work environment. Alloy — the OpenTelemetry-native Grafana agent — is the collector layer, configured to receive OTLP from local apps and forward to the backends. The whole thing runs in the cluster and survives a node restart because the storage volumes are on the ThinkStation's SSD array.

The useful part is the bench side of it. When I'm testing a new firmware revision on one of the ESP32 sensors, the device publishes metrics over MQTT, a small bridge service subscribes and re-emits as OTLP, and the readings show up in the same Grafana instance as the software services. It's a silly amount of infrastructure for monitoring a temperature sensor, but it means the toolchain I test at the bench is the same toolchain I use at work, which is the whole point of having the space.
