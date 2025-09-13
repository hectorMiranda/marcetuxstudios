---
layout: post
title: "Designing a studio power monitor board"
date: 2025-09-12
author: marcetux
tags: [kicad, electronics, pcb, hardware, homelab]
---
The UPS replacement got me thinking about power monitoring more seriously. The current
setup reports power draw as a single number from the UPS SNMP interface, which is fine
for total consumption but tells me nothing about which rack component or bench circuit
is responsible for spikes. I want per-circuit monitoring: current sense on each of the
three bench circuits and the rack feed, feeding the Pi cluster for real-time Grafana
dashboards.

The design is a four-channel current sensor board: INA228 precision current/power
monitors on each channel, talking I2C to an ESP32-C3, with current transformers
clamped around each circuit's hot wire rather than in-line shunts. Clip-on current
transformers mean I don't have to break the circuits to install the monitor. The INA228
has enough dynamic range and resolution to see everything from a 2W idle to the 2kW
peak when the oscilloscope and hot-air station are both on.

The KiCad schematic is done; routing starts this weekend. The PCB is simpler than the
RF board — no controlled impedance, no RF considerations, four copies of the same
measurement circuit — which means I can focus on the mechanical layout and make sure
the CT clamp connectors are accessible without pulling the board. Clean cable routing
at installation time is worth two extra hours of layout.
