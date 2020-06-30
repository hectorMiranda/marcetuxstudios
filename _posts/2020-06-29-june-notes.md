---
layout: post
title: "June notes"
date: 2020-06-29
author: marcetux
tags: [meta, retrospective]
---
June was a reliability month. The AKS upgrade is done, properly, with PodDisruptionBudgets
that should have been there from day one. The liveness/readiness probe fix stopped
a recurring annoyance that we'd been attributing to "load spikes" — it was really
our own probe configuration killing healthy pods. API Management rate limits are now
the first line of defense against partner misconfiguration. The theme is infrastructure
that fails better instead of infrastructure that requires babysitting.

The ghost load is gone. Forty watts of standby devices, now cut on a schedule via
a Tasmota plug. The Grafana dashboard finally matches the physical reality of what's
actually on in the house. It's a small thing, but it closes a three-month loop that
was quietly annoying me.

Tailwind has earned a place in my personal-project toolkit. The side project it's
on looks better than anything I've built with Bootstrap at equivalent effort, and
the discipline of deciding what actually deserves a named class is useful regardless
of framework. July: I want to look at the .NET 5 preview — RC1 is rumored for fall,
and I want to know what's in it before it ships.
