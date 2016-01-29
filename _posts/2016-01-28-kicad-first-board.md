---
layout: post
title: "First KiCad board after years on Eagle"
date: 2016-01-28
author: marcetux
tags: [kicad, eagle, pcb, electronics, hardware]
---
Autodesk's acquisition of Eagle last year — and the subsequent licensing change from
a one-time fee to a subscription — was the push I needed to actually migrate to KiCad.
I'd been putting it off because I know Eagle, my parts library is in Eagle, and
switching tools mid-project is expensive. But a subscription I'd have to renew forever
for a tool I use on hobby boards is a different equation than the old hobbyist license.

KiCad's schematic capture and PCB layout tools are similar enough to Eagle that the
concepts transfer, but different enough that the first board took three evenings instead
of one. The footprint library situation is genuinely better — KiCad's community-
maintained library is large and the footprints are reliable — and the 3D view is a
pleasure that Eagle never had. Routing feels a bit different but competent. The real
win is the push/shove router, which nudges traces out of the way rather than refusing
to move when there's room.

The first board out of KiCad is a small breakout for an ESP8266 with a few passives,
nothing I haven't made before. The Gerbers look right in the previewer; they're queued
at the fab now. What I'm actually testing is whether my workflow survives the migration,
not the board itself. Two more designs and I'll feel like I'm in KiCad rather than
merely visiting.
