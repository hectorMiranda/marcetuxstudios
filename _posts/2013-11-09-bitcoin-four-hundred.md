---
layout: post
title: "Bitcoin at $400 and the caching patterns of a bubble"
date: 2013-11-09
author: marcetux
tags: [cdn, caching, bitcoin, performance, http]
---
It was $200 two weeks ago. Bitcoin is at $400 tonight and the traffic to anything
crypto-adjacent is remarkable in the dashboards. I'm not trading — the mechanics of
wallets and exchanges have my engineer's brain more interested than the price — but
the price chart and the CDN traffic chart are now correlated enough that I'm certain
price volatility is what drives traffic spikes, not the other way around.

The pattern that's interesting from a caching perspective: origin servers that should
be handling this gracefully are getting crushed, and the ones that survive are the
ones where the CDN can absorb the read traffic with a short TTL. I've written about
this twice now; the Bitcoin run is just making the lesson replayable every few days.

What I notice about my own reaction: I'm watching the traffic patterns and the caching
behavior more than the price. The price is either going up or it isn't, and I don't
have a better model for which than anyone else. But the traffic patterns are something
I can reason about: they follow human attention, which spikes when prices move sharply,
which is a predictable pattern. If I were building a crypto price service today I'd
design for the assumption that traffic triples whenever the price moves by more than
15% in an hour. That's not a guess; it's what the dashboards show every time.
