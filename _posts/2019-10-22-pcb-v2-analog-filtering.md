---
layout: post
title: "Energy monitor board v2, improving the analog front end"
date: 2019-10-22
author: marcetux
tags: [electronics, altium, pcb, analog, energy]
---
The energy monitor v1 board had a noise problem I mentioned in September: at low current draws, the RMS calculation was drifting because the ADC on the ESP32 was sampling a noisy waveform without any hardware conditioning. The solution is a simple second-order low-pass filter on each CT input before the ADC pin — a Sallen-Key topology using a dual op-amp and four passive components per channel. The math works out to a -3dB cutoff around 300Hz, which passes the 60Hz fundamental and most of the harmonics I care about while attenuating the high-frequency noise.

The Altium schematic update took a couple of evenings. The op-amp choice fell on the TLV2372, a rail-to-rail output device that runs from 3.3V — matching the ESP32's supply — and doesn't need a negative rail for the CT's AC waveform because the CT output circuit adds a DC bias at the midpoint of the supply. The layout change is non-trivial: adding six components per channel in the same board area requires rerouting. I took the opportunity to also add a TVS diode on each CT input as overvoltage protection, which was an oversight on v1.

Boards ordered. If the noise floor improves as much as the simulation suggests, I should be able to detect the standby draw of individual appliances rather than just the large circuit-level loads. The coffee maker's standby mode at 3W on a 20A circuit is noise on v1; it should be readable on v2. Data granularity is what the project was always trying to get to.

*Update: the boards arrived and the analog front end performs as simulated. The noise floor dropped dramatically — standby loads in the 3–12W range are now stable readings rather than noise. The SO-8 op-amp hand-soldering was fiddly but the rework iron made it manageable. Full writeup in the November post.*
