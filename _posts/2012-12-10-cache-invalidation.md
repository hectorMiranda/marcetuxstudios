---
layout: post
title: "The other hard problem: cache invalidation"
date: 2012-12-10
author: marcetux
tags: [caching, redis, architecture]
---
Phil Karlton's line — two hard things in CS are cache invalidation and naming — is
a joke until you ship a Redis cache and a customer emails that their dashboard shows
yesterday's number an hour after it changed. Then it's a Tuesday.

Three strategies, roughly in order of how much they hurt. **TTL** is the laziest and
often enough — the data is wrong for at most N minutes, and for daily rollups nobody
cares. **Write-through / explicit eviction** — when the source changes, delete the
key — is correct but couples your writers to your cache, and you *will* forget a
write path. **Versioned keys** — bake a version or timestamp into the key so new
data lands under a new key and the old just expires — sidesteps invalidation
entirely by never updating in place.

I've settled on TTL for the rollups and versioned keys for anything that must be
fresh-on-change. The one I avoid is hand-maintained eviction; that's the path that
emails you on a Tuesday.
