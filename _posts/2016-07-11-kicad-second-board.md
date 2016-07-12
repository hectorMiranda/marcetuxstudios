---
layout: post
title: "Second KiCad board and getting faster at the workflow"
date: 2016-07-11
author: marcetux
tags: [kicad, pcb, electronics, hardware, home]
---
The second KiCad board is ordered. This one is less of a "migration validation" and
more of a real design: a small environmental monitor board with an ESP8266, a BME280
for temperature/humidity/pressure, and a battery management circuit that the first
board delegated to a TP4056 module. Integrating the charger IC onto the board rather
than using a module saves space and is more satisfying to design.

The workflow is genuinely faster the second time. Schematic entry went in an evening
because I have a KiCad parts library now — I added the ESP-12F and BME280 footprints
from the first board and they carry over automatically. The push/shove router handles
the dense center section around the ESP-12 better than I'd managed by hand in Eagle;
I let it route those traces and hand-corrected the ones it chose poorly. The 3D view
is still the best part — I can see whether the connector is going to clear the
battery before the board arrives from the fab.

The BME280 is the sensor upgrade I've wanted on the kitchen board since January. The
DHT22 is reliable but slow (one reading per 2 seconds) and has no pressure channel.
The BME280 reads in under 10 ms and the pressure data lets me track weather fronts
passing through Culver City, which is useless information I want anyway. New boards
arrive in three weeks.
