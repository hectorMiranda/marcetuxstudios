---
layout: post
title: "Cache-Control, and what the edge actually does with it"
date: 2013-02-16
author: marcetux
tags: [http, caching, cdn, performance]
---
Spending my days around a CDN has made me opinionated about a header most apps treat
as an afterthought: `Cache-Control`. The edge does exactly what you tell it and
nothing you wish you had, so the header is a contract, and most origins write a sloppy
one.

The two numbers that matter aren't the same. `max-age` tells the *browser* how long
to hold a copy; `s-maxage` tells *shared caches* — proxies and the CDN edge — and
overrides `max-age` for them. So you can say "browsers, keep this a minute; edge, keep
it an hour," which is usually exactly what you want: a long edge cache that absorbs
the load, a short browser cache so a forced refresh gets something fresh-ish. Most
origins set one number and wonder why the edge behaves oddly.

The other half is invalidation, and the honest truth is the edge can't read your
mind. Either you version the URL so a change *is* a new object — the trick from the
asset-fingerprinting work — or you call the purge API when content changes. Long TTLs
plus deliberate purges beat short TTLs every time: you get the cache hit ratio *and*
control. Cache aggressively, purge precisely. The default of "cache nothing because
invalidation scares me" just means you're paying origin bandwidth for content that
never changes.
