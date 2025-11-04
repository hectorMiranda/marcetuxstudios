---
layout: post
title: "Installing the power monitor in the studio rack"
date: 2025-11-03
author: marcetux
tags: [electronics, hardware, homelab, kicad, studio]
---
The power monitor boards arrived and I did the installation during Saturday's rack
downtime. Clip-on current transformers, two per circuit, clamping the hot wire at the
breaker end of each branch. The ESP32-C3 on the monitor board connects to the same
network the rest of the lab is on; ESPHome firmware, I2C to four INA228 channels, data
into Home Assistant every ten seconds.

First measurements: the rack draws 38W at idle (NAS spinning down, Pi cluster idle,
switch active). The workstation adds 220W under load, which is the GPU compute headroom
I expected. The bench circuits are calmer than I thought — the oscilloscope at idle is
about 15W, the Weller station at operating temperature is 40W, and the bench power
supplies add whatever the device under test is drawing. Nothing surprising, which is the
good outcome from a power monitoring project.

The interesting finding: there's a 6W draw on the rack circuit that I can't account for
during periods when everything looks powered down. Tracking that down is this week's
puzzle. The monitoring system is already earning its keep.
