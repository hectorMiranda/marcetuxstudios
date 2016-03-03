---
layout: post
title: "The KiCad board came back from the fab"
date: 2016-03-02
author: marcetux
tags: [kicad, pcb, electronics, hardware, esp8266]
---
The ESP8266 breakout I sent to the fab in January arrived in a small padded envelope
Wednesday night. Ten boards, green soldermask, white silkscreen, the footprints exactly
where I put them. I stared at them for a few minutes before picking up the soldering
iron, the same way I always do when something abstract becomes physical.

Everything worked first try, which is suspicious and also great. The USB-to-serial is
enumerated correctly, the 3.3V LDO is within a millivolt, and the ESP8266 accepts
firmware over the UART without argument. The KiCad footprints I was most nervous about
— the ESP-12 module's castellated pads — lined up cleanly. I must have measured them
correctly. The push/shove router I liked in KiCad produced sensible trace geometry
that I didn't have to second-guess after the fact.

Nine spare boards of a thing that works is a good problem. I'll use one for the kitchen
sensor project, leave two as spares, and stuff the rest in a drawer where they will
eventually become prototyping substrates for unrelated experiments. The real outcome
here is that KiCad is now a tool I trust rather than one I'm visiting. The next board
will go faster.
