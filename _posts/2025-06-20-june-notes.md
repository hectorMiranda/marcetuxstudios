---
layout: post
title: "June notes"
date: 2025-06-20
author: marcetux
tags: [meta, retrospective, studio]
---
Half the year done. The studio is fully operational — six months ago it was a raw
concrete room, now it has a working bench, a instrumented lab rack, an Altium workstation
with a real GPU, and CO2 sensors telling me when to open a window. The buildout ended
up being the right decision for the work. I'm more productive here than I've been at any
previous setup.

The RF front-end board layout is done and at the fab. The long-running spectrum monitor
project is finally getting the hardware it deserves. On the consulting side the platform
engineering and agent architecture work keeps coming; the market has settled into a
pattern where teams that shipped LLM features in 2024 are now figuring out how to
maintain and improve them, which is a different and more interesting class of problems.

The thing I'm thinking about going into the second half: the agent memory problem is not
solved, and the solutions I've built are bespoke per engagement. There should be a
cleaner reusable pattern, and I want to write one that's production-worthy before I
describe it here.
