---
layout: post
title: "Routing the board, and sending it to a fab"
date: 2013-02-09
author: marcetux
tags: [electronics, eagle, pcb, hardware]
---
Saturday project: finished routing the bench-logger board in Eagle and, for the
first time, actually sent something off to be manufactured. Breadboard to copper, the
January resolution, made real.

Routing turned out to be the puzzle game I suspected. Two copper layers, and the
trick is getting traces from everywhere to everywhere without crossings — when two
need to swap sides you drop a via and jump to the bottom layer. I poured a ground
plane on the bottom, which both saves routing the ground net by hand and is good
practice for noise. The autorouter exists; I routed the signal traces by hand anyway,
because I wanted to *understand* the board, not just have one.

Then the leap: generate the Gerber files — the standard format every fab speaks — run
them through a viewer to make sure I didn't hand the factory garbage, and order ten
boards from one of the cheap overseas houses for about the price of lunch. Ten,
because the minimum is ten and one mistake means the other nine are spares. Two to
three weeks on a slow boat. Now the agony: staring at files I can no longer change,
certain I got a footprint backwards.
