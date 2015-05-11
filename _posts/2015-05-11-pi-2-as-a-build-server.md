---
layout: post
title: "Using the Pi 2 as a lightweight build server"
date: 2015-05-11
author: marcetux
tags: [raspberrypi, devops, ci, hardware]
---
A Jenkins instance on the Raspberry Pi 2 sounds like a bad idea on paper — 1 GB RAM,
four slow ARM cores — and it mostly is for anything CPU-intensive. But for the ESP8266
firmware project it's the right size: the builds are small, the build frequency is low,
and the Pi 2's quad-core headroom is enough to compile a 20 KB Arduino sketch without
struggling.

Jenkins on ARM is just Jenkins: download the WAR, run with Java, done. The ARM
constraint is memory — keep the heap under 512 MB, run only one build at a time, and
don't install the kitchen-sink plugin list. The pipelines I set up are simple: pull the
Git repo, run `arduino-cli compile`, run a Python-based lint pass on the config, archive
the binary. If it fails, a Slack notification goes to the home channel. If it passes,
an OTA push triggers automatically on the development node.

The thing I got out of this besides automated builds: the Pi 2 with a small SSD
attached is a surprisingly capable headless Linux server for tasks that don't involve
CPU saturation. Mosquitto broker, InfluxDB, Grafana, and now Jenkins, all running
comfortably together. The energy cost is maybe 7 watts. It's not a build server I'd
run professional CI on, but for a home project where the alternative is manual and error-
prone, it's exactly enough.
