---
layout: post
title: "DFM rules in Altium before you send the board out"
date: 2021-06-14
author: marcetux
tags: [electronics, altium, pcb, hardware]
---
The second LoRa repeater board came back from the fab with a subtle silkscreen
problem: reference designators for two SMD passives were landing under the body
of an adjacent QFN. Not a functional failure — the silkscreen is just ink — but
the board looked sloppy and the assembly house would have struggled to identify
the parts during hand rework. Fixable, annoying, and preventable.

Altium's DRC includes silkscreen violation checks, but I hadn't configured the
clearance rules to match the fab's guidelines. The default DRC runs with Altium's
built-in rules; the fab's DFM requirements are different — usually tighter on copper-
to-edge and soldermask expansion, sometimes looser on silkscreen clearance over pads
because they know their process. The correct workflow is: download the fab's design
rule file, import it before you run DRC, and treat any violation as a blocker before
you generate Gerbers.

The third board is in the queue now with a proper fabricator-specific rule set
imported from their site. The silk check caught four overlaps I'd missed, one of
which would have been a legitimate assembly problem. That's the right return on
twenty minutes of setup: catching mistakes you'd have discovered at the receiving
dock instead of at the design workstation. DFM is not a checkbox at the end; it's
a constraint you run against every time you move a component.
