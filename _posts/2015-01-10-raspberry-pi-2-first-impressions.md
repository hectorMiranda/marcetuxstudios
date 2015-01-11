---
layout: post
title: "Raspberry Pi 2, first impressions from the bench"
date: 2015-01-10
author: marcetux
tags: [raspberrypi, electronics, linux, hardware]
---
The Pi 2 landed and the difference is not subtle. The original Pi's single-core ARM11
was fine for a headless sensor node, but anything that involved Python plus networking
plus a little processing overhead had that wait. The Pi 2's quad-core Cortex-A7 with
1 GB RAM is a different category — it runs a full Raspbian desktop without embarrassing
itself, which means it's actually useful as a low-power build machine or a home
automation hub.

The upgrade path was painless: same GPIO pinout, same form factor, same power supply.
I swapped the SD card from an existing bench-logger node, it booted on the first try,
and the only config change was updating the architecture-specific build flag for the
temperature sensor driver. The node now handles MQTT publish cycles and a lightweight
HTTP status endpoint without the CPU pegging on polling storms. That headroom matters.

The thing I keep coming back to is how far this platform has come from being a
educational toy. A $35 quad-core Linux computer with GPIO, HDMI, USB, and a massive
community-maintained software library is a serious tool. I'm already planning a
dedicated home-automation hub build: Pi 2 as the broker and dashboard host, ESP8266
nodes as the sensors — each cheap enough to leave in a wall permanently.
