---
layout: post
title: "Home Assistant, first week"
date: 2017-01-14
author: marcetux
tags: [homeassistant, iot, raspberrypi, home]
---
I've had a Raspberry Pi sitting on a shelf since the Pi 3 came out, destined to be
"the home automation hub" I kept not building. Home Assistant changed that in about an
afternoon. The project is a Python-based home automation platform that runs on the Pi,
discovers most smart devices on its own, and exposes everything through a clean web UI
and a YAML configuration file I can put under git.

The first integration was my Hue lights, which took about three lines of YAML. The
second was the Nest thermostat, which required an API key but was otherwise identical in
complexity. What surprised me is that the abstraction holds: from Home Assistant's
perspective a Hue bulb and a smart switch and a sensor are all "entities," and I write
automations against entities without caring what protocol sits underneath.

The Pi is running Raspbian, Home Assistant in a virtualenv, and I set it up as a systemd
service so it comes back after reboots. The config lives in a private git repo. Next
weekend: wiring in an ESP32 temperature sensor over MQTT. The MQTT broker is already
running on the Pi alongside everything else, and Home Assistant has native MQTT support.
That's the part I've wanted to do for months.
