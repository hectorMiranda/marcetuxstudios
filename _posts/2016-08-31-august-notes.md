---
layout: post
title: "August notes"
date: 2016-08-31
author: marcetux
tags: [meta, retrospective]
---
August had three things I'm proud of and one I should have done earlier. .NET Core 1.0
is on a production endpoint and behaving; the BME280 board is the best hardware I've
built — integrated battery management, three sensor channels, works first try. Webpack 2
tree shaking is real and the Angular 2 project bundle got meaningfully smaller.

The circuit breaker was the "should have done this earlier" item. Two years of
occasional metadata API outages amplified into queue backups by timeout accumulation, and
the fix was one afternoon of work. I wrote it down as a pattern rather than an incident
because the gap between knowing a pattern and having it in production is apparently
"waiting for the right amount of pain." CloudWatch custom metrics are on the pipeline;
the p95 duration alarm fired once in August and was a legitimate heads-up about a worker
instance with a disk performance issue.

September: Angular 2 final is supposedly imminent. I want to be running on the release
before the end of the month, not pinned to an RC. Also want to explore whether the
circuit breaker state logic can be extracted into a small library the other services can
use without copying the implementation.
