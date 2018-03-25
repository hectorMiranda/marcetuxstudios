---
layout: post
title: "Drone build log: motors and ESCs"
date: 2018-03-25
author: marcetux
tags: [electronics, drone, hardware, hobby]
---
The Shenzhen order finally arrived — 2306 motors and four BLHeli_32 ESCs — and the
drone went from "bag of parts" to "something that might fly" in a Saturday afternoon.
Soldering the motor leads to individual ESCs is the repetitive part: eighteen joints
per ESC, sixty-four holes in the frame PDB pads, nothing difficult but nowhere to
rush without a cold joint that will vibrate loose at 15,000 RPM.

BLHeli_32 is the thing worth noting. The 32-bit ESC firmware handles DSHOT600 and
sends telemetry back over the same wire — RPM, temperature, and voltage per motor in
real time to the flight controller. Betaflight picks that up and can display it in the
OSD and use RPM filtering to clean up the gyro signal. The old way was tuning
empirically until the shaking stopped; now the FC knows the actual motor RPM and can
notch out the harmonic frequencies directly. The theory is sound and the tune was
faster than anything I'd achieved by feel before.

Maiden flight is scheduled for next weekend in the empty lot behind the house. The
only thing missing is a better FPV camera, but I'll fly in line-of-sight first — no
point learning to fly with a latency budget added on top of the skill budget.
