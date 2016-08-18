---
layout: post
title: "The circuit breaker pattern for dependent service calls"
date: 2016-08-17
author: marcetux
tags: [architecture, reliability, backend, patterns, api]
---
The transcoding pipeline calls an external metadata API to enrich the job record after
processing — film data, poster URLs, that kind of thing. For months, when that API was
slow or down, our workers backed up because every job was waiting the full HTTP timeout
before failing. The queue depth grew; on-call woke up; we manually killed requests;
things cleared. The pattern is embarrassing because the fix is well-known and I'd just
never implemented it here.

A circuit breaker is state around a dependency: in the **closed** state requests pass
through normally. When failures exceed a threshold within a time window, the breaker
opens. In the **open** state, requests fail immediately without hitting the dependency —
the worker knows the metadata API is unhealthy and doesn't spend the timeout finding
out. After a configured interval, the breaker moves to **half-open** and lets one test
request through; if that succeeds, the breaker closes; if it fails, it opens again.

The key benefit in our case is the timeout-amplification problem. If the metadata API
is timing out at 10 seconds and we have 50 concurrent workers, 50 × 10 seconds of
stuck threads is a 500-second backlog that takes minutes to clear after the dependency
recovers. An open breaker fails in milliseconds, keeping workers available for jobs
that don't need the metadata path. I implemented it with a small state object backed
by Redis; the breaker state is shared across all workers. Fast failure is a feature.
