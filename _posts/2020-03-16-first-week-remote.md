---
layout: post
title: "First full week remote and what broke first"
date: 2020-03-16
author: marcetux
tags: [remote, devops, process, architecture]
---
We went fully distributed last week. The bank's infrastructure was ready for some
people working remote; it was not ready for all people working remote simultaneously.
VPN concentrator capacity was the first thing to go, then the jump-server bottleneck
for the internal deployment tooling. Both are fixable. The interesting failures were
the process ones.

A lot of coordination that happened incidentally — someone walking past someone's
desk — turned out to be real work. On-call handoffs that were "hey, watch this
service, it's been flapping" delivered verbally now need a written artifact. Incident
channels on Teams got busier because the question "is anyone seeing X?" can't be
answered by looking at who's at their machine. We're documenting runbooks we should
have had before, not because we're virtuous but because they're now actually required
instead of optional.

The infrastructure that aged best is the stuff we made async by accident: the GitHub
Actions pipelines that run without someone babysitting, the Terraform state backend
that lets two people apply without coordinating a window, the OpenAPI contracts that
let client and server teams build in parallel. None of it was designed for distributed
work specifically, but "doesn't require someone to be present" is a property that
compounds well. That's the engineering agenda for the next few months.
