---
layout: post
title: "The boards arrived and I only got one footprint wrong"
date: 2013-03-02
author: marcetux
tags: [electronics, eagle, pcb, hardware]
---
The ten boards from the fab landed on my doorstep Saturday morning, and I spent an
embarrassing amount of time just staring at them. Green fiberglass with gold pads,
exactly the Gerber files I sent — which is the banal miracle of outsourced PCB
manufacturing. Clean silkscreen, consistent drill hits, not a trace lifted.

Then the moment of truth: break out the BOM and populate one board. Most of it went
fine — resistors seated, the barrel connector happy, the decoupling caps in their
places. The ATmega328 landed correctly. And then the FT232 footprint: I'd mirrored the
chip orientation in Eagle and didn't catch it in the Gerber review. Pin 1 is on the
wrong side. Nine unusable boards for the USB-serial chunk of the circuit, one board
bodge-wired around it.

Live lesson: double-check datasheet pin 1 marking against the fab's viewer, not just
against Eagle's default. The bodge wire works fine, the logger logs, and I'm already
laying out v1.1 with the correction. One wrong footprint out of forty on a first-ever
board is, I'm told, a pretty good run.
