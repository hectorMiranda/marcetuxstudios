---
layout: post
title: "February notes"
date: 2018-02-26
author: marcetux
tags: [meta, retrospective]
---
Short month, reasonably dense. The TypeScript `strict` migration was overdue and the
three hours of errors were a good investment — the kind of cleanup that's painless in
a dedicated sitting and miserable if you let it accumulate. Health checks are now
wired correctly to the Kubernetes probes and we haven't had that "pod is alive but
broken" situation since. The JWT claims shift cut a measurable database round trip
from the hot path.

The drone is two-thirds built — motors and ESCs in the mail from a vendor in Shenzhen
who takes two weeks and $4 per motor, which remains impossible to reconcile with the
physics of what they're doing. The KiCad board for the pulse-oximeter logger is on
hold; I ran into a footprint mismatch with the MAX30102 breakout and need to redraw
before ordering.

Work news: the hiring conversations from January resolved. We have a junior backend
dev starting in March and another mobile contract being negotiated. Growing a team
at a startup is a different job than being the team, and I'm still figuring out which
parts of my old job I need to hand off versus which I'm holding onto out of habit.
That's March's problem.
