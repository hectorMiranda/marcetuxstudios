---
layout: post
title: "Redis in front of SQL"
date: 2012-11-05
author: marcetux
tags: [redis, caching, performance, architecture]
---
The reporting rollups are read constantly and change slowly — the textbook case for
a cache, and Redis is the tool I reached for. An in-memory key/value store with real
data structures and a network interface, sitting between the app and SQL Server.

The pattern is cache-aside: check Redis for the key, return it on a hit; on a miss,
query SQL, store the result with a TTL, return it. The TTL is the whole design
conversation — it's how long you're willing to serve slightly stale numbers, and
for daily bandwidth totals "a few minutes" is generous.

What sold me beyond plain caching is that Redis isn't just strings — sorted sets,
hashes, counters with atomic increments. A "top customers by bandwidth" widget is a
sorted set, maintained as data arrives, read in one call. The cache stops being a
shelf and starts being a little query engine.
