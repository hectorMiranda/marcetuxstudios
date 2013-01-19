---
layout: post
title: "Designing my first PCB in Eagle"
date: 2013-01-18
author: marcetux
tags: [electronics, eagle, pcb, hardware]
---
2013 resolution, started early: get off the breadboard and design an actual circuit
board. The bench logger has lived on jumper wires for months and it's one bumped
wire away from a debugging session. Time to make it permanent in Eagle.

The workflow is a two-act play that confused me at first. **Act one is the
schematic** — draw the logical circuit, the Arduino-compatible MCU, the sensor
header, the power regulation, connected by named nets. **Act two is the board** —
the same components as physical footprints you arrange and route copper traces
between. The schematic is *what connects to what*; the board is *where it physically
goes*. Eagle keeps them in sync, screaming at you if the board contradicts the
schematic.

Routing is the part that's secretly a puzzle game — fitting traces on two layers
without crossing, using the ground plane well. I'm not paneling and ordering yet;
this first one is about learning the tool without releasing magic smoke. But seeing
my breadboard mess become a clean schematic already feels like progress.
