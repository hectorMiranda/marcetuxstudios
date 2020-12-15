---
layout: post
title: "Home lab 2020 - the year the Pi cluster got serious"
date: 2020-12-14
author: marcetux
tags: [homelab, kubernetes, raspberry-pi, retrospective]
---
At the start of 2020 I had a single Pi 4 running a handful of Docker containers.
At the end I have a three-node k3s cluster running Kubernetes workloads, Longhorn
distributed storage, Prometheus and Grafana for observability, and a full sensor
pipeline from ESP32 hardware through MQTT into InfluxDB with dashboards for energy,
temperature, and solar production. It got serious.

The forcing function was, paradoxically, the move to remote work. When my desk is
also my lab and I have ten hours a week instead of weekend-only time, the projects
that were "eventually" became "this weekend." The k3s cluster was standing-up-a-
curiosity in January and running the actual sensor stack by February. The Prometheus
stack was aspirational in spring and was monitoring itself by September. Remote work
removed the commute and added back the lab time.

The thing the home lab gives that work doesn't: the freedom to break things
completely and fix them from scratch. I've corrupted the InfluxDB volume twice,
relearned Longhorn backup restoration once, and had k3s elect the wrong leader after
a three-node outage. All of those are learning I wouldn't have gotten in a managed
AKS cluster. The lab is where I break things so production doesn't have to be.
