---
layout: post
title: "HTTP/2 server push in theory and in practice"
date: 2016-07-22
author: marcetux
tags: [http2, performance, frontend, cdn, network]
---
HTTP/2 has been in browsers for over a year and most CDN providers support it on their
edge now, including CloudFront. The feature everyone mentions first is server push:
the ability for the server to proactively send assets the client will need before the
client asks for them. Theory: the browser requests the HTML; the server pushes the CSS
and JS along with the response; the browser saves a round trip. Practice: it's
finicky, and I've spent a week convincing myself the hype is slightly ahead of reality.

The push works as described for the first visit. The problem is cache interaction: if
the browser already has the JS in cache, you've pushed bytes it didn't need, wasting
bandwidth and potentially losing to the cache anyway. The "conditional push" story —
knowing whether the client has a resource before pushing it — is not standardized.
CDN vendors have different implementations and the right behavior requires cooperation
between origin and edge that's hard to express in today's tooling.

What's more reliably useful is HTTP/2 multiplexing, which doesn't require any new
server-side behavior: multiple resources share a single connection, eliminating the
per-connection overhead that made asset concatenation and spriting necessary under
HTTP/1.1. With HTTP/2, the "bundle everything into one JS file" optimization becomes
less critical — smaller modules fetched concurrently is a reasonable alternative. I
enabled HTTP/2 on the CloudFront distribution; server push stays off until the cache
interaction story is cleaner.
