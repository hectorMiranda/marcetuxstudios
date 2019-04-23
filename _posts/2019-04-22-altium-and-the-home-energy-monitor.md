---
layout: post
title: "Altium and the home energy monitor project"
date: 2019-04-22
author: marcetux
tags: [electronics, altium, pcb, energy, homelab]
---
I switched from KiCad to Altium last year and spent the fall just learning the workflow differences. April is when I actually finished a board in Altium that I'm confident is correct: a current-sensing board for the home energy monitor project I've been talking about since January. It's a simple board — three SCT-013 clamp-on current transformer inputs and an ESP32 to read them — but getting all the way through Altium's constraint manager and out the other side with clean Gerbers felt like a milestone.

The energy monitor idea is to track the mains circuits in the apartment by clipping non-invasive CTs around the load wires. The ESP32 reads the AC waveform, computes RMS current, and publishes to MQTT every ten seconds. A home automation dashboard picks it up and graphs it. The CT sensors don't require touching mains wiring — the clamp goes around the wire, no cutting — which is the version of this project I can do in an apartment without calling an electrician.

Altium's constraint editor is its real advantage over KiCad for my workflow: I define a design rule for trace clearance, track width by current carrying capacity, and minimum via annular ring, and the DRC catches violations while I route rather than at the end when it's too late. I got the footprint on the SCT connector slightly wrong the first time — verified in 3D view before ordering Gerbers, which saved me a spin. The boards are out to fabrication. Power monitoring with actual data sounds a lot better than guessing which circuit is the apartment's main power consumer.
