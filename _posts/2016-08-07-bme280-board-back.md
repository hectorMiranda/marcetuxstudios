---
layout: post
title: "BME280 board back and barometric pressure data in Grafana"
date: 2016-08-07
author: marcetux
tags: [electronics, kicad, esp8266, sensors, home]
---
The second KiCad board came back from the fab last week — the environmental monitor
with the BME280 and integrated TP4056-style battery management circuit. Soldered it up
Saturday morning. The battery management IC is a MCP73831 in a SOT-23-5 package, which
is small enough that I used the hot-air rework station rather than the iron; the other
passives are 0402s that I'm getting more comfortable with.

Everything worked: the charger IC brings the LiPo to 4.19V (the programming resistor
gives 500mA charge current, right where I wanted it), the 3.3V LDO is stable, and the
BME280 responds to I2C at the expected address. Firmware is the same architecture as
the kitchen sensor — wake, read, publish to MQTT, sleep — just extended to push three
topics: temperature, humidity, and pressure. The Grafana dashboard got a third panel.

The pressure trend is the thing I didn't expect to care about and now check daily. A
steady pressure drop over 12 hours reliably precedes the marine-layer marine fog that
rolls into Culver City from the coast. The sensor predicted the last two weather changes
before I noticed them through the window. Useless data until you have it; then it's
the kind of thing you can't imagine not having.
