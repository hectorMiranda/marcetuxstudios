---
layout: post
title: "PCB bring-up: the pulse oximeter board lives"
date: 2018-11-14
author: marcetux
tags: [electronics, kicad, pcb, hardware, hobby]
---
The pulse-oximeter board came up on the first try, which should feel more routine than
it does. Hot air, solder paste, a visual check under a loupe, and the I2C address at
0x57 appeared in `i2cdetect` on the first boot. A year from first KiCad file to
working board, which is embarrassingly slow, but the board is clean.

The MAX30102 is a combined pulse-ox and heart-rate sensor that communicates over I2C
and includes its own ADC and LED drivers — no external analog circuitry required, which
is why the schematic is simpler than I expected. The raw data is two 18-bit values,
red and IR light reflected from the finger, and the pulse detection algorithm runs in
software on the Pi rather than in hardware. I'm using the SparkFun Arduino library
ported to Python, which is functional but not optimized; the interrupt pin on the IC
signals when data is ready and I'm polling on a timer instead, which adds jitter.

The board logs to a SQLite database via a small Python script and I pull the data
into Home Assistant as a custom sensor. Blood oxygen and heart rate on the dashboard
next to the temperature and humidity sensors. Medically useless at this precision, but
a good end-to-end exercise in I2C bringup, signal logging, and display. The footprint
lesson from September paid off — everything sat flat and reflowed correctly.
