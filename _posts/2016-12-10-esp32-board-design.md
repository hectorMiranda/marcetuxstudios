---
layout: post
title: "Designing the first ESP32 board in KiCad"
date: 2016-12-10
author: marcetux
tags: [esp32, kicad, pcb, electronics, hardware]
---
The ESP32 board is on the KiCad canvas. I'm using the ESP32-WROOM-32 module — a
castellated module like the ESP-12F, which I've soldered before and know the footprint
tolerances on. The board is more ambitious than my previous ESP8266 designs: onboard
USB-to-serial via a CP2102N, a 3.3V LDO with appropriate decoupling, I2C header for
the BME280 (external on this board, not integrated), and a LiPo connector with the
MCP73831 charger circuit I used on the second ESP8266 board.

The CP2102N is what I didn't have on earlier boards — a USB interface chip that gives
you a serial port without a separate USB-to-serial dongle. Program and debug the ESP32
by plugging in a micro-USB cable, which is much better than the FTDI-adapter dance.
The RTS and DTR lines from the CP2102N drive the EN and IO0 pins through transistors
for automatic bootloader entry; you don't hold a button while plugging in, you just
click upload in the IDE. This is how the official DevKitC works; I'm replicating the
pattern on a board that fits my form factor.

The KiCad workflow is now genuinely fast — I'm not looking up keyboard shortcuts
anymore, the library is populated with the parts I use repeatedly, and the 3D view
catches clearance issues before they're in Gerbers. The board will go to the fab after
the holidays. I'd like to have it back before I start the next job.
