---
layout: post
title: "Response caching in ASP.NET Web API"
date: 2014-01-14
author: marcetux
tags: [dotnet, webapi, caching, rest, performance]
---
The CDN portal's reporting endpoints were taking the kind of hit you get when a dozen
browser tabs all ask for the same thirty-day bandwidth rollup on the same customer at
once. The data doesn't change minute-to-minute; recomputing it on every request was
pure waste. The fix was caching the API response, but wiring it correctly took more
thought than I expected.

Web API doesn't come with response caching built in the way MVC does — you're working
closer to the HTTP layer. The approach that held up is an action filter that checks
an in-process cache before running the action, stores the result with a TTL afterward,
and sets the `Cache-Control` header on the response so a downstream CDN edge can cache
it too. The cache key is the full request URL including query string; that's coarse
enough that URL differences always yield a miss, which is the safe default.

The nuance is varying on the authenticated user. A rollup scoped to one customer
must not be served to another, so the cache key incorporates the user identity. The
`Cache-Control` header gets `private` on user-scoped responses so the CDN edge doesn't
conflate them. Write the key so a wrong hit is impossible, then let the TTL handle
freshness. The caching layer that used to scare me is mostly bookkeeping once you're
precise about the key.
