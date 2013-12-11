---
layout: post
title: "The stack at the end of 2013"
date: 2013-12-10
author: marcetux
tags: [retrospective, dotnet, angular, architecture, engineering]
---
Two posts from October covered the pieces and how they fit. December is the time to
note what I'd do differently.

The thing I'd start earlier: async all the way down. The Web API controllers went async
in September, nine months into the year, and converting them was a mechanical change
that improved load handling measurably. "Do it later when we need it" is a fine rule
for a lot of optimizations; async I/O in ASP.NET is not that kind of optimization — it's
architectural and converting it late means touching every controller. Start async.

The thing I wouldn't change: the decision to type the Angular service layer in TypeScript
from February onward. The type definitions were rough and I've patched them several
times, but catching property-name mismatches at compile time rather than three clicks
deep in the browser is a net win that compounds every sprint. The investment pays off
faster than it looks like it will in week one.

The thing I still haven't done: a proper async queue for the heavy report generation.
It's on the list for 2014. The reports are synchronous, they hold a web worker thread
for up to thirty seconds on large date ranges, and they'll be the first thing that breaks
when load triples. I know exactly which seam to cut: a queue in Redis, a separate worker
process reading it, a polling endpoint for the browser to check completion. The design
is obvious; the quarter just didn't have room for it.
