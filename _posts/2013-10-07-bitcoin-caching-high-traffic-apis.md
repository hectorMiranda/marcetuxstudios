---
layout: post
title: "Bitcoin price feeds and what high-traffic read APIs teach you about caching"
date: 2013-10-07
author: marcetux
tags: [http, caching, cdn, rest, performance]
---
Bitcoin crossed $140 in April and has been climbing all year — it's at $140 again this
week on the way somewhere higher, and the number of applications polling exchange price
APIs every second is apparently substantial enough that at least one exchange has been
vocally complaining about bot traffic. Watching this from inside a CDN company is
instructive.

A price feed API is a read-heavy endpoint where the data changes frequently but not
instantaneously. The correct caching strategy for this is a short positive TTL — thirty
to sixty seconds — at the edge, with `stale-while-revalidate` behavior if the origin
supports it. Clients get a response in edge latency rather than origin latency for
nearly every request; the origin sees one update request per TTL period per edge PoP,
not one per client per second. The exchange handles thousands of clients instead of
millions of polling loops.

The lesson generalizes: any read-heavy endpoint where "slightly stale is acceptable"
is a candidate for short positive caching at the edge. The friction is organizational
— developers who own the API want to say "our data is authoritative and fresh," which
is true but irrelevant to whether a sixty-second-old price helps the caller as much as
the real-time price does. Designing cache-friendly APIs requires the API owner and the
CDN to agree on what "fresh enough" means for each resource. That conversation is
easier if the API sets the `Cache-Control` header with intent rather than leaving the
edge to guess.
