---
layout: post
title: "A year-end performance audit"
date: 2012-12-21
author: marcetux
tags: [performance, profiling, dotnet, sqlserver]
---
Quiet week before the holidays, so I did the thing that never fits into a normal
sprint: sat down and *measured* the portal end to end instead of guessing where it's
slow.

The findings were humbling in the usual way — the slow things were not the things I
"knew" were slow. The expensive report I'd been eyeing was fine; a chatty little
lookup on the dashboard was firing forty queries because of a lazy-loaded property
in a loop (the classic N+1). A miniprofiler trace made it obvious in about ten
minutes, after months of vague "the dashboard feels heavy" complaints.

The lesson I relearn every year: **profile before you optimize.** Engineering
intuition about performance is reliably wrong, because the bottleneck is usually
something boring you stopped looking at. Measure, fix the top item, measure again.
I closed three real issues this week and didn't write a single clever algorithm —
just deleted an N+1 and added one index the plan asked for.
