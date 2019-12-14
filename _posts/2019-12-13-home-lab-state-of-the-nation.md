---
layout: post
title: "Home lab state of the nation, end of 2019"
date: 2019-12-13
author: marcetux
tags: [raspberry-pi, homelab, networking, energy, hardware]
---
A December tradition: take stock of what the home lab looks like and where it's going. The 2019 inventory is meaningfully different from 2018. The Pi 4 arrived in June and took over as the primary monitoring host. Two Pi 4s now, plus the older Pi 3 B+ handling the MQTT broker. The energy monitor v2 is producing accurate data and the Grafana dashboards are showing me things about my power usage I genuinely didn't know before.

The infrastructure stack: InfluxDB for time-series on the Pi 4, Mosquitto for MQTT, Home Assistant running on the second Pi 4, Nginx as a reverse proxy fronting the dashboards behind a local DNS entry and a self-signed cert I trust on the home devices. The whole thing is managed with Ansible playbooks now — I can rebuild any of it by running a playbook against a fresh Pi image, which is how I know what I actually have rather than how I think I have it. Nothing undocumented in production, even at home.

The Altium projects for 2019: the current-sensing board went through two spins and the v2 is in daily use. The next project forming in my head is a LoRa-based sensor node for monitoring the outdoor power circuits — the two exterior outlets on the balcony that I want to track separately from the main panel. LoRa over a sub-GHz band would let me mount a node outside without running Ethernet. Design would start in January on the PCB; the LoRaWAN gateway is already running on the Pi 3.
