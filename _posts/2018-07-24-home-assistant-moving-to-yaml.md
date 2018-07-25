---
layout: post
title: "Home Assistant moving everything to YAML configuration"
date: 2018-07-24
author: marcetux
tags: [homeassistant, homeautomation, iot, hardware, hobby]
---
The home automation setup has drifted for a year — devices added through the GUI,
automations tweaked through the GUI, a state that I can no longer recreate if I have
to rebuild the Pi. The motivation to fix this finally arrived when the SD card
started throwing filesystem errors and I realized I had no reproducible config.

The fix is what I should have done from the start: move everything into YAML, put the
config directory in a Git repository, and treat Home Assistant as code. Every device
is a YAML entity. Every automation is a trigger-condition-action document. The
`configuration.yaml` includes package files so each domain — lighting, climate, sensors
— is a separate file rather than a sprawling monolith. A restore is a git clone and
a container restart.

The Betaflight and Kubernetes experience made this obvious in retrospect. Any system
configuration worth keeping is worth tracking. The GUI is for exploration; YAML is
for production. The sensor network in the house now runs five ESP32 nodes reporting
temperature and humidity to MQTT, which Home Assistant subscribes to and displays on
a Lovelace dashboard I built over the weekend. The SD card in the Pi is now a
deployment artifact, not a museum piece.
