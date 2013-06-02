---
layout: post
title: "V1.1 boards are back and everything is correct"
date: 2013-06-01
author: marcetux
tags: [electronics, eagle, pcb, hardware]
---
The v1.1 bench-logger boards arrived Saturday and I populated one immediately —
all forty components, no bodge wires, no surprises. The FT232 footprint is correct. The
UART header is exactly where I put it on the schematic. The power continuity check I
did before sending to fab was worth the twenty minutes: every bypass cap is on the
right net, the ATmega has its VCC and GND, and the first power-on showed the LED coming
on and the USB-serial enumerating on my laptop without drama.

The logger logs. I can see temperature, supply voltage, and the two ADC channels on the
USB-serial terminal without a bodge wire in sight. The Raspberry Pi connects through the
new UART header with a single 4-pin cable — clean, plug-and-pull, the way I'd intended
from the start. Nine boards left to populate; I'll do a few more and keep one as a
permanent bench instrument.

The thing this build taught me: the checklist from the v1.0 failure is the most valuable
output of a failed prototype. Not the dead boards — those are just expensive feedback.
The checklist is the part that follows you to every future board. Good hardware takes
at least two spins; knowing *why* is what makes the second spin actually better.
