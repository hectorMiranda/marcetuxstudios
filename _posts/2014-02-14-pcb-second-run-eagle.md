---
layout: post
title: "Second Eagle board, fixing the first run's mistakes"
date: 2014-02-14
author: marcetux
tags: [electronics, eagle, pcb, hardware, maker]
---
The bench-logger boards from last February finally told me what I got wrong — one
header footprint was mirrored, so plugging in the UART connector required crossing two
wires with a paper clip and hoping. Not a showstopper but the kind of thing that makes
you embarrassed in front of yourself every time you use it.

The second revision fixed the footprint, added decoupling caps I'd optimistically left
off the first pass, and moved a test point that was under a component standoff and
therefore useless. Eagle has an ERC and DRC check for a reason; I ran both this time and
actually addressed every warning instead of clicking them away. The DRC flagged a trace
that came within two mils of a pad — legal but too close for comfort. Nudged it wider
and felt better about it.

Ordered the second run from the same fab, same price, ten more boards. They should
land in three weeks. The lesson is that the first board is never the real board — it's
a prototype that tells you what the real board should be. I know that intellectually
and still needed to learn it empirically. Now I budget two fabrication runs into any
new PCB project.
