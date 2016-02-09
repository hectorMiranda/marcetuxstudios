---
layout: post
title: "The Vary header and why it surprises CDNs"
date: 2016-02-08
author: marcetux
tags: [cdn, http, caching, performance]
---
We started serving different video quality tiers based on the `Accept-Encoding` and
a custom `X-Client-Type` request header, and immediately found that the CDN was serving
the wrong variant to some clients. The culprit was `Vary`. `Cache-Control` tells the
edge how long to hold an object; `Vary` tells it *which response headers differentiate
cached copies*. Without the right `Vary`, a cached response for a desktop client gets
served to a mobile one.

The correct response includes `Vary: Accept-Encoding, X-Client-Type`, which instructs
the CDN to maintain separate cache entries for each unique combination of those header
values. The catch is that many CDN providers either ignore `Vary` headers that include
custom fields or implement it in ways that destroy cache hit ratios — because every
unique header combination is a separate cache key. You can end up fragmenting the cache
so badly that the effective TTL might as well be zero.

The fix we landed on: stop varying on the request header, normalize the client type in
the origin and encode it in the URL path instead. `/video/mobile/clip.mp4` and
`/video/desktop/clip.mp4` are unambiguously different objects to every cache layer, no
`Vary` semantics required. It's a bit more URL surface but the caching behavior is
predictable. When you need the CDN to do the right thing, make the right thing
unambiguous.
