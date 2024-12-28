---
layout: post
title: "December electronics time and the sensor project"
date: 2024-12-28
author: marcetux
tags: [electronics, kicad, esp32, hardware, homelab]
---
The holidays gave me the uninterrupted bench time I'd been rationing all year. The
sensor board that's been in KiCad since October is now laid out, reviewed for obvious
DRC errors, and ready to send to fab. It's an ESP32-S3 board — custom footprint for
a BME688 environmental sensor and a SHT45 for cross-validation, a small LiPo charge
circuit, UART header for programming, and an MQTT stack that reports to the home lab.
Nothing the ESP32 sensor community hasn't done, but mine, with the footprints I
chose and the power budget I calculated.

The KiCad layout went faster in the studio than any board I've laid out on the kitchen
table. Having the oscilloscope visible reminds me to think about signal return paths.
Having space to spread out the datasheet printouts next to the laptop means I'm not
toggling between tabs. Small things, but they compound. I routed it in about four
hours over two evenings, which is maybe half the clock time of the last comparable
board in a cramped setup.

The board goes to JLCPCB on the 30th. The usual tension: staring at Gerbers I can no
longer change, hoping the through-hole pads I resized to 1.2mm from 1.0mm will make
soldering easier rather than just different. I've been sending boards to fabs for
eleven years — the gap between "sent" and "received" is still the same. January will
say whether the BME688 footprint is correct.
