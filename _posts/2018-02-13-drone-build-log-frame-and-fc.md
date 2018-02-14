---
layout: post
title: "Drone build log: frame and flight controller"
date: 2018-02-13
author: marcetux
tags: [electronics, drone, hardware, hobby]
---
Spent most of Valentine's weekend assembling the 5-inch racing frame I've been
accumulating parts for since December. Not racing — I have no ambition to fly
competitively — but the racing-class builds are where the interesting engineering is
right now: compact, properly fused, tunable. The hobby-grade RTF quads that showed
up at every mall kiosk last year taught me nothing about how they work.

The frame is carbon fiber, which is lightweight and very good at absorbing crash
energy by shattering arms — so you buy spare arms upfront, not in denial. The flight
controller is a Betaflight-compatible F3 board with an integrated PDB and a built-in
current sensor, which means I get battery voltage and current draw in the OSD without
wiring a separate sensor module. Betaflight's configurator runs in the browser via
WebUSB now, which is a nicer debugging surface than I expected.

The interesting part from a systems angle is the motor ESC protocol. I chose DSHOT
instead of the old analog PWM because DSHOT is digital — no calibration, no signal
interference, bidirectional telemetry from the ESC back to the flight controller. The
frame and FC are built; motors, ESCs, and VTX are next. Progress is slow because I'm
doing this between midnight and when I should be sleeping, but that's where hobby
projects live.
