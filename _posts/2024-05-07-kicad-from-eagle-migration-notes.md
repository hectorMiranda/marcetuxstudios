---
layout: post
title: "KiCad migration notes after years on Eagle"
date: 2024-05-07
author: marcetux
tags: [kicad, eagle, electronics, pcb, hardware]
---
I moved from Eagle to KiCad a few years ago when Autodesk changed Eagle's licensing
and the community was clearly migrating. In 2024 I'm fully comfortable in KiCad 7
and I want to write down the things that tripped me up in the transition, because I
keep meeting people who are still on Eagle out of inertia.

The biggest conceptual shift: KiCad separates schematic symbols from PCB footprints
at the library level. Eagle bundles them. In Eagle, a component in your schematic
has an implicit footprint. In KiCad, a component in the schematic has a symbol,
and you assign a footprint separately — either during schematic entry or as a batch
step before layout. This feels like extra work until you realize it means one symbol
can map to multiple footprints without duplicating the schematic entry. The 0402
resistor symbol works for 0402, 0603, and 0805; you assign the actual package to
the specific design.

The scripting console is where KiCad earns serious respect. Python, exposed to the
board database. I automated a panelization step that would have been forty minutes of
manual work in Eagle. The community script library is extensive. If you're still on
Eagle because you know all the keyboard shortcuts: the KiCad keyboard shortcuts are
different and it takes two weeks to stop grabbing the wrong key. Then it's fine.
