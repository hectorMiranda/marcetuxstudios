---
layout: post
title: "The v1.1 board layout and not repeating the footprint mistake"
date: 2013-04-15
author: marcetux
tags: [electronics, eagle, pcb, hardware]
---
Started the v1.1 bench-logger layout this weekend with one rule: check every IC
footprint against the physical part before sending to fab. I made a simple checklist:
part number, package code in Eagle's library, datasheet page with pin 1 marking, test
against a DIP socket or the bare chip on the bench. Fifteen minutes per IC, saves a
four-week rework cycle.

The FT232RL footprint is fixed — pin 1 is now correctly aligned with the dot on the
silkscreen. While I was in there I rerouted the power traces wider (from 12mil to 24mil
for the 5V rail) and added the test points I wished I'd had when debugging the bodged
board. A 2.54mm pad on each power rail and on the ATmega's SPI pins means I can probe
without alligator-clipping to a header leg.

The new addition is a proper UART header exposed as a 4-pin 2.54mm connector: GND, 5V,
TX, RX. Plug a FTDI cable or a Pi directly without the bodge wire. The v1.0 lesson is
that every signal you don't explicitly expose is one you have to solder a wire to later.
Expose the interfaces you expect to use. I'm probably sending this one to the fab by end
of month.
