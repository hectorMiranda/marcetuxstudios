---
layout: post
title: "V1.1 boards ordered and a better Eagle checklist"
date: 2013-05-24
author: marcetux
tags: [electronics, eagle, pcb, hardware]
---
Sent the v1.1 bench-logger board to fab Friday. This time I had the checklist from the
v1.0 lesson — every IC footprint verified against the physical part — and also ran the
ERC and DRC passes in Eagle until they came back clean rather than until I got tired
of reading warnings. Three weeks again, same slow-boat overseas house.

The new thing I did this time was a manual continuity check on the power net before
exporting Gerbers. Eagle's highlight-net feature lets you select all the traces and
pads on a net by name. I stepped through VCC and GND visually, making sure every IC
had a VCC and GND pad connected with no floating stubs. Found one: a bypass cap near
the ATmega had its GND pad connected to the wrong net — the net name was right in the
schematic symbol but the PCB footprint hadn't been updated to match after I moved a
power net. ERC missed it because the schematic was clean; the mismatch was between
schematic and layout.

The lesson is that the ERC and DRC catch their own classes of errors but the human
check catches the schematic-to-layout gap. These are three different verification
steps — electrical rules, physical design rules, schematic-layout consistency — and no
single one catches everything. Run all three. The three weeks of waiting is long enough
to regret skipping a check that took twenty minutes.
