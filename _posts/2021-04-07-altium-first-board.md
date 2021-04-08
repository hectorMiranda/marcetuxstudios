---
layout: post
title: "First real board in Altium Designer"
date: 2021-04-07
author: marcetux
tags: [electronics, altium, pcb, hardware]
---
I migrated from KiCad to Altium at work a year ago and have been using it for
client boards, but this weekend I finally ran my own LoRa repeater board through
Altium from schematic to Gerber. KiCad is genuinely good software for free; Altium
is what happens when the tooling budget is not zero and it shows in the things that
matter on a complex board.

The DRC is the thing I notice most. KiCad's DRC is serviceable; Altium's DRC is
paranoid in the best way — it flags clearance violations, silkscreen overlap,
via-in-pad issues, and net antenna warnings before I've even thought about running
DFM. On a four-layer board with a ground plane and controlled-impedance traces, that
paranoia catches the mistake I'd have discovered after waiting three weeks for the
boards to arrive. The rule-set is also design-intent: I define the board before I
route it, not after.

The footprint library management is the other win. My KiCad project always had
a "project library" folder of footprints I'd copied from somewhere and half-
modified. Altium's managed libraries mean the SX1278 footprint I used last year
is the exact same footprint I used today, version-controlled, with the datasheet
pinned to it. When a component gets a revision from the supplier, I update the
library once. Boring exactly the way version control is boring — it solves a
problem that hurts badly enough that you forget there was a problem once it's solved.
