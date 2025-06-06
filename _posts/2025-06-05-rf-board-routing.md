---
layout: post
title: "Routing the RF front-end board in Altium"
date: 2025-06-05
author: marcetux
tags: [altium, rf, electronics, pcb, hardware]
---
The RF front-end board for the spectrum monitor has been on the routing table in Altium
for a month, and I finished the layout this week. Controlled-impedance routing at 50
ohms for the coaxial signal path, a ground plane pour with stitching vias around the
RF section to keep the analog and digital ground domains from coupling, and a balun
footprint from the parts library that I had to rebuild from scratch because the
manufacturer's land pattern was wrong in the version I downloaded.

The lesson I keep relearning about RF PCBs: the layout is the design. You can have a
perfect schematic and a broken board because a trace took the wrong path and picked up
noise from a switching regulator two inches away. The Altium 3D viewer is useful here —
not for aesthetics, but for seeing the physical path of the antenna trace relative to
the components that could interfere with it. I rerouted the LNA output trace twice
because the 3D view made it obvious it was running over a digital clock line.

Gerbers go to the fab next week, four-layer stackup with the controlled-impedance
parameters I specified in the stack manager. This one will have a bring-up procedure
with the spectrum analyzer before I solder the active components — hard lesson from a
board two years ago where I assembled first and debugged the layout second.
