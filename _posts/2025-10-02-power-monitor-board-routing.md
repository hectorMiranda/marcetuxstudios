---
layout: post
title: "Routing the power monitor board"
date: 2025-10-02
author: marcetux
tags: [kicad, electronics, pcb, hardware, homelab]
---
The power monitor board routing went faster than expected — about four hours on Saturday,
spread across two sessions because I kept stopping to check the INA228 datasheet for
layout recommendations. Current sense boards have one critical layout rule that's easy
to miss: the sense traces from the current transformer secondary must reach both
differential inputs of the measurement IC before any other connections, with matched
trace length and width. Skew on those traces adds offset error; Kelvin routing
eliminates it. The DRC pass caught nothing, which usually means I got it right or
there's a mistake I haven't seen yet.

The four-channel layout ended up as a 70x55mm board, single-sided components for hand-
assembly, with the CT connector positions along one long edge so the wires exit toward
the panel box. Each INA228 channel has its own 100nF bypass capacitor within 2mm of
the power pin — the datasheet is specific about this and I've learned not to argue with
power supply decoupling recommendations on precision analog hardware.

Gerbers go out tomorrow. Board back in two weeks. I'll install the current transformers
during a rack power-off, which means scheduling the downtime around the consulting work.
Running a home lab you actually depend on has its own coordination overhead.
