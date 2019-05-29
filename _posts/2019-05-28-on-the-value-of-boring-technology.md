---
layout: post
title: "On the value of boring technology"
date: 2019-05-28
author: marcetux
tags: [architecture, philosophy, process, teams]
---
A junior architect on the team wanted to adopt a reactive message-stream processor for what was, on close inspection, a batch job that runs twice a day. The system he proposed was technically interesting; it was also a distributed system with five moving parts, a new operational domain for the team, and a learning curve measured in months. I've been on both sides of that conversation enough times to have a position.

The boring option — a scheduled Azure Function that reads from a queue — takes an afternoon to build, five minutes to explain to the on-call engineer who's never seen it before, and the debugging session when it fails at 3 AM is reading a log file instead of understanding reactive backpressure semantics while exhausted. The function has one job and one failure mode. New technology has two learning curves: the thing itself, and how it fails, which is always different from how the documentation suggests it might fail.

I am not categorically against new things. I'll reach for gRPC when JSON overhead matters at scale. I adopted Dynatrace because manual threshold configuration doesn't scale to fifty services. But the adoption decision should be driven by a problem the boring option genuinely can't solve, not by the interesting-ness of the solution. The best architecture decision I've made at the bank this year was telling a team that their existing SQL Server query, with one index added, would handle their load for the next two years. They wanted a new system. They got a working one.
