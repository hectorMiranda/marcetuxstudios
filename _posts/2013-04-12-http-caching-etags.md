---
layout: post
title: "ETags and conditional requests for the API layer"
date: 2013-04-12
author: marcetux
tags: [http, caching, webapi, rest, performance]
---
The dashboard polls a few read-heavy endpoints even after the SignalR push work —
configuration endpoints, things that don't need real-time push but change occasionally.
They were returning full responses on every request regardless of whether anything had
changed. ETags are the HTTP mechanism for skipping that response when nothing changed,
and wiring them into Web API is a short afternoon.

An ETag is a token — a hash of the response body works fine — sent in the `ETag`
response header. On the next request the client sends `If-None-Match: <that-token>`.
The server checks whether its current content still hashes to the same value; if it
does, it returns `304 Not Modified` with no body. The client uses its cached copy. The
round-trip still happens but the body transfer disappears, which matters on
mobile-connected clients and saves origin processing time.

The implementation is a Web API `ActionFilterAttribute`: compute the hash after the
action runs but before the response serializes, check the request header, either
short-circuit with 304 or set the `ETag` header and proceed. One attribute, applied
to a handful of controller classes. The clients benefit automatically without knowing
ETags exist — they just see faster responses on unchanged data. Conditional requests
are one of those HTTP features that's been there forever and is worth using properly.
