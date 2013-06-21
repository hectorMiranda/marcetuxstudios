---
layout: post
title: "Actually reading the HTTP spec and what I got wrong"
date: 2013-06-21
author: marcetux
tags: [http, rest, webapi, architecture]
---
The new HTTP 1.1 spec drafts are circulating under RFC 7230–7235, splitting the old
RFC 2616 into six focused documents, and I spent a couple of evenings reading them.
RFC 7231 covers semantics — methods, status codes, headers — and it corrected a few
things I'd been doing by convention rather than by spec.

The one that stung: I'd been returning `200 OK` with an empty body for `DELETE`
requests, because that's what an earlier tutorial showed. The spec is clear: a
successful `DELETE` that has nothing meaningful to return should be `204 No Content`.
A `200` implies there's a response body worth reading. The difference matters for
caching proxies and for client code that branches on status code. We had a client that
deserialized a `null` from the empty JSON body — `200` with `{}` is a different signal
than `204` with nothing.

The method idempotency definitions also clarified something I'd been fuzzy on. `PUT`
is idempotent: calling it twice with the same payload produces the same result as
calling it once. `POST` is not. We had a few endpoints that were receiving `POST`
requests that were clearly idempotent operations — "set the cache rules for this zone"
— and should have been `PUT`. Not a crisis, but the mismatch between method semantics
and actual behavior is the kind of thing that bites a client developer who reads the
spec and expects the obvious behavior.
