---
layout: post
title: "HTTP/2 and what multiplexing actually fixes"
date: 2015-02-21
author: marcetux
tags: [http, performance, networking, frontend]
---
HTTP/2 was finalized this month. The headline feature people repeat is multiplexing —
multiple requests over one TCP connection simultaneously — but I wanted to understand
what problem it's actually solving, not just name-drop the spec.

HTTP/1.1 has head-of-line blocking at the application layer: a response has to finish
before the next request over that connection can start. Browsers work around this by
opening 6–8 parallel connections per origin, which is resource-intensive and still
limited. HTTP/2 frames all requests and responses over a single connection with explicit
stream IDs, so a slow response on stream 3 doesn't block stream 4's response from
arriving. The connection overhead drops because it's one TLS handshake instead of eight.

The immediate practical change is that HTTP/1.1 performance tricks reverse. Domain
sharding — splitting assets across multiple domains to get more parallel connections —
becomes actively harmful because it prevents multiplexing. Sprite sheets and JS bundles
help less because the browser can make many small requests without the connection
overhead penalty. HTTP/2 makes the honest thing (small, focused resources) faster
rather than punishing it. We're not deploying it yet — server support is maturing — but
I'm flagging the asset pipeline work so we don't bake in more assumptions we'll have to
undo.
