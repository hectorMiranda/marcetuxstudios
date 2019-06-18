---
layout: post
title: "Raspberry Pi 4, first impressions"
date: 2019-06-17
author: marcetux
tags: [raspberry-pi, homelab, hardware, linux]
---
The Raspberry Pi 4 landed this week and it's a meaningful step. The 4 GB model I ordered boots, runs, and is noticeably faster than the Pi 3 B+ it's replacing in the home lab — the Cortex-A72 cores and the switch to USB 3.0 and gigabit Ethernet with actual bandwidth behind it make it feel like a different class of device. The GPIO is the same 40-pin header, so my energy monitor HAT from the current-sensing board project fits without modification.

The things that surprised me: the USB-C power situation is finicky with certain chargers due to an errata in the initial PCB design — the USB-C port isn't fully spec-compliant and some USB-C chargers that do e-marker negotiation won't power it. I hit this with the first cable I tried and spent twenty minutes confused before finding the note in the forums. Get a proper Pi power supply or a basic USB-C adapter without e-marker support. The USB 3.0 ports are a genuine upgrade for a NAS-style workload; plugging in an external SSD and running `dd` showed sustained speeds the Pi 3 USB 2.0 bus could never hit.

Running the energy monitor on the Pi 4 with the current-sensing board: MQTT publishing is working, Home Assistant is picking it up, and I now have a graph of the apartment's circuit loads over time. The Pi 4's extra grunt will let me run more InfluxDB retention windows without the storage performance becoming the bottleneck. This is the home lab upgrade I was waiting for.
