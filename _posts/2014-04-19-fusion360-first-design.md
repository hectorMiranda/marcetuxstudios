---
layout: post
title: "Designing a real part in Fusion 360"
date: 2014-04-19
author: marcetux
tags: [3dprinting, cad, fusion360, hardware, maker]
---
Saturday was Fusion 360 and the filament spool guide I promised myself in March. The
short version is that parametric CAD is genuinely better than downloading someone else's
STL when the part needs to fit your specific printer, because "close enough" in a
downloaded part usually means an hour of post-processing with sandpaper.

Fusion 360 is free for makers and lighter than SolidWorks, which I tried years ago and
abandoned when it wanted a license key to save. The parametric model means dimensions
are variables: change the bracket width and the mounting holes move with it. That
sounds like a nice-to-have until you print a test piece, measure it against the actual
machine, and realize the slot needs to be three millimeters wider. In Fusion I edit
one dimension, regenerate, export. In a downloaded STL I'm scaling the whole part and
hoping proportions hold.

The part printed on the second attempt — first attempt had a support structure I didn't
design and the slicer didn't add automatically, leaving an overhang that drooped. I added
a 45-degree chamfer instead of a sharp overhang and the second print needed no support
at all. The spool hasn't fallen off since. Designing for the process, not around it.
