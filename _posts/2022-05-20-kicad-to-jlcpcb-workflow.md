---
layout: post
title: "KiCad to JLC PCB: the workflow that finally stuck"
date: 2022-05-20
author: marcetux
tags: [electronics, kicad, pcb, hardware]
---
I've been using KiCad for the serious PCB work since I migrated from Eagle a couple of
years ago. The workflow to get from a KiCad design to a board in my hands has evolved
with each project, and after a few rounds with JLC PCB I've settled on a process that
loses me exactly zero boards to misunderstood Gerber options.

The export flow: in KiCad's PCB editor, run "Plot" to generate the Gerber layers
(copper top/bottom, silkscreen, solder mask, edge cuts), then run "Generate Drill Files"
separately for the drill file — JLC wants them in the same zip as the Gerbers. The
critical settings are "Use Protel filename extensions" for the layer naming, and "PTH
and NPTH in a single file" for the drill. Load the zip into JLC's Gerber viewer before
ordering — that viewer is the last sanity check, and it catches the occasional footprint
that places copper where only the silkscreen should be.

The part that saves the most money on small runs is JLC's SMT assembly service, which
requires a BOM in their CSV format and a centroid file (component placement). KiCad
can export both; the BOM needs the JLC part numbers added, which is a one-time exercise
per component you introduce. Getting the first board back fully assembled, having only
supplied the KiCad files, is still a small satisfaction every time. A design that lives
in files comes back as a physical thing, and that is the point.
