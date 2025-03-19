---
layout: post
title: "First KiCad session in the new studio"
date: 2025-03-18
author: marcetux
tags: [kicad, electronics, pcb, studio, hardware]
---
Did my first real PCB work in the studio last Tuesday — a small environmental sensor
board in KiCad, picking up a project that had been sitting half-routed since November
when everything was still in boxes in the bedroom. There is something measurable about
working at a bench that's the right height, under light that's actually good, with the
oscilloscope 18 inches to the left and not on a different table. The board got done in
an evening. I think that's a studio dividend.

The board is a CO2 and temperature/humidity sensor on an ESP32-C3, meant to feed the
Home Assistant instance in the rack. The C3 is smaller and cheaper than the original
ESP32 for this application, and the single-sided antenna design is cleaner when the
enclosure is plastic. KiCad 8 has some incremental improvements to the routing tools —
interactive router constraints, better pad connections in the 3D view — that make the
experience noticeably smoother than the version I was running in 2023.

The Gerbers go to the fab this week. I've learned to do a proper DRC pass, check the
3D view for mechanical clearance, and then do one final visual scan of the Gerber viewer
before hitting order. The three-step pre-send ritual has saved me from two footprint
errors over the years. Boards back in three weeks.
