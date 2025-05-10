---
layout: post
title: "Altium on the studio workstation"
date: 2025-05-09
author: marcetux
tags: [altium, electronics, pcb, studio, hardware]
---
KiCad handles most of what I do and I'm genuinely fond of it, but there's one class of
project — anything with a controlled-impedance stackup, a dense BGA footprint, or a RF
section — where I reach for Altium. The license has been sitting on the old laptop since
2022 and moving it to the studio workstation was worth doing properly rather than
deferring. Altium on a machine with a real GPU and enough RAM is a different experience.

The current project is a small RF front-end for a spectrum monitoring board — part of
the long-running home RF observation system that's been in various states of completion
for three years. The Altium stackup manager is the thing I can't replicate in KiCad for
this class of board: you specify the dielectric and copper thickness per layer, and the
impedance calculator updates every trace you've flagged for controlled impedance in
real time. For microstrip and coplanar waveguide work that's not a luxury.

The studio gives me contiguous working time in the evenings without the desk-to-bench
context switch that killed momentum in the bedroom setup. The RF board will take another
month to route properly, but at least now the tool and the workspace are not the
constraint.
