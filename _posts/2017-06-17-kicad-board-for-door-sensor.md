---
layout: post
title: "KiCad board design for the garage door sensor"
date: 2017-06-17
author: marcetux
tags: [kicad, pcb, electronics, esp32, hardware]
---
The garage door sensor has been running on a breadboard and Dupont wires long enough
to be embarrassing. The components are simple — an ESP32 module, a LiPo charging
circuit, a reed switch footprint, a status LED, and a JST connector for the battery —
but the breadboard version is delicate in a way that anything mounted in a garage
should not be. KiCad board time.

The design I settled on is a single-sided board that fits inside a small project box.
The ESP32 module is through-hole for replaceability — if the module burns, I don't want
to rework SMD pads in a garage. The LiPo charger is a TP4056-based module on breakout
pins, again through-hole. SMD resistors and capacitors for the LED circuit because I
have them in stock and the pads are on the simpler end of SMD. KiCad's ERC catches
the nets I always forget to label; the DRC catches the trace widths that are too narrow
for the current I'm running.

Sent to JLCPCB this week. Five boards for roughly the price of a restaurant lunch.
The fab files from KiCad have been reliable — I've never had a board come back with a
fabrication error since I switched from Eagle, which I attribute to KiCad's cleaner
Gerber export and to actually running the DRC before submitting. The boards arrive in
two weeks and the breadboard prototype stays alive until they do.
