---
layout: post
title: "The sensor board came back from the fab"
date: 2025-04-09
author: marcetux
tags: [electronics, kicad, esp32, hardware, homelab]
---
Three weeks after sending the Gerbers, the CO2 sensor boards arrived. Ten of them, as
always, because that's the minimum and now I have nine spares. The footprints are right.
The antenna keepout on the ESP32-C3 is clear. The SCD40 CO2 sensor pads line up. I
assembled the first board Wednesday evening at the new bench — the Weller station in its
proper place, proper light overhead — and it was one of those sessions where the setup
feels like it finally matches the work.

First power-on: the ESP32-C3 enumerated over USB, the SCD40 responded to I2C, and the
first CO2 reading came back at 812 ppm (studio, windows closed, me in it for an hour —
plausible). The firmware is ESPHome, which talks directly to Home Assistant over the
network and requires exactly zero custom code for this hardware. I've become a convert
to ESPHome for any sensor node that isn't doing something exotic; the configuration YAML
handles the hardware abstraction and the HA integration handles the dashboard. I only
write firmware when the platform can't do it.

Four boards will go into the studio and the adjacent space; the rest are insurance.
Reading data is already showing up in Grafana alongside the power and temperature metrics
the NAS pushes. The studio is becoming properly instrumented.
