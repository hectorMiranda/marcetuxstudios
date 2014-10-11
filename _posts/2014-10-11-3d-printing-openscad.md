---
layout: post
title: "OpenSCAD for programmatic 3D models"
date: 2014-10-11
author: marcetux
tags: [3dprinting, cad, openscad, hardware, maker]
---
Fusion 360 is the right tool when you're designing something with complex geometry that
benefits from a parametric history tree. It's overkill for a sensor enclosure that's
a box with a hole in the top. OpenSCAD is the right tool for that — a programming
language that describes geometry through code rather than a GUI mouse session.

OpenSCAD describes models as combinations of primitives. A box is `cube([30, 20, 10])`.
A cylinder is `cylinder(h=5, r=3)`. Boolean operations compose them: `difference()` cuts
one shape from another, `union()` combines them, `intersection()` keeps only the
overlap. A sensor enclosure is a cube with a cylinder subtracted where the sensor
pokes through and a slot subtracted where the lid slides on. In fifteen lines of code
with variables for dimensions, I have a model that regenerates to a new size by changing
two numbers at the top.

The ESP8266 enclosure I designed took about forty minutes and came out exactly to spec
on the first print, which has not been my experience with Fusion 360 models that I
had to rebuild when a dimension was wrong. Parametric code is faster to iterate than
parametric GUI when the geometry is simple. The right tool question for 3D modeling
is the same as for any other tool: complicated surfaces belong in Fusion 360; boxes
with holes belong in OpenSCAD.
