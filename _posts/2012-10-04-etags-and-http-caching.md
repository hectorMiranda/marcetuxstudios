---
layout: post
title: "ETags and the caching the web already gives you"
date: 2012-10-04
author: marcetux
tags: [http, caching, rest, performance]
---

Following the reporting-is-a-read theme: the cheapest response is the one you never
send. HTTP has had conditional requests forever, and I keep meeting APIs that
ignore them.

Two headers do most of the work. **`ETag`** is a version stamp for a representation
— a hash, a row version, whatever. The client sends it back as
`If-None-Match`, and if nothing changed you answer `304 Not Modified` with an empty
body. The bytes stay home; the client reuses what it has. **`Cache-Control`** with
`max-age` lets you say "this is good for five minutes, don't even ask again."

For my bandwidth rollups this is a gift. Yesterday's totals are immutable — slap a
long `max-age` and a strong `ETag` on them. Today's are volatile — short max-age,
revalidate often. Freshness becomes a per-resource decision encoded in headers, not
a cache layer you bolt on later.

The discipline: pick ETags you can compute cheaply. If generating the validator
costs as much as generating the body, you've saved bandwidth and spent it on CPU.
