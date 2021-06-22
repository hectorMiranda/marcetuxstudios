---
layout: post
title: "Caching in layers and what each layer is for"
date: 2021-06-21
author: marcetux
tags: [architecture, caching, performance, dotnet]
---
A performance review of the customer-data service found four separate caching
mechanisms layered on top of each other, none of them documented and at least one
actively working against the others. In-memory cache in the service, Redis cache
at the integration layer, CDN cache for the static response variants, and a
database query cache that we'd enabled and forgotten about. The customer had four
stale-data windows, all different, all configured independently.

Each cache layer has a correct use. In-memory cache within a process is for data
that's expensive to compute and cheap to discard — it lives and dies with the
process. Distributed cache like Redis is for data that multiple instances need to
share or that needs to survive a process restart. CDN cache is for the subset of
responses that are truly identical for a class of users and where the origin
bandwidth saving is real. Database query cache is usually a footgun — it's too far
down the stack to have useful freshness semantics and it interferes with the
optimizer.

The rationalized version of the service has two layers: Redis with a 60-second TTL
for account summary data that all three service instances need to agree on, and
nothing else cacheable until we measure a real need. The CDN cache is off for
authenticated data. The database query cache is disabled. Three fewer sources of
stale truth. Caching is not optimization by default; it's a trade between freshness
and cost that needs to be made consciously.
