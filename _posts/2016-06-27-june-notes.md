---
layout: post
title: "June notes"
date: 2016-06-27
author: marcetux
tags: [meta, retrospective]
---
.NET Core 1.0 actually shipped in June as promised — a rare thing in this industry —
and running a real API on it under Linux inside a container felt like a milestone
arriving after watching the previews for a year and a half. The tooling is `project.json`
for now, which is a known temporary state, but the runtime is solid. Docker Compose
for local dev was this month's "why didn't we do this sooner" change; onboarding time
to a working environment dropped from an hour of instructions to two minutes of commands.

The Grafana dashboard is the operations view now — four panels, deliberately narrow, and
the first dashboard I've built that people actually look at voluntarily. CORS is not a
security layer: I wrote it down because I've had the same conversation three times this
year and writing it down is faster than the next conversation.

July: the Angular 2 release candidate situation is ongoing and I want to track whether
the router stabilizes enough to be trustworthy. Also want to do a proper cost audit of
the CDN spend — I have a feeling we're paying for a lot of cache misses we could avoid.
