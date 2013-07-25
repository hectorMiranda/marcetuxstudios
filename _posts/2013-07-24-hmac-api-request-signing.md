---
layout: post
title: "HMAC request signing for the customer API"
date: 2013-07-24
author: marcetux
tags: [security, webapi, authentication, http, rest]
---
Some partners access the customer API with server-to-server calls where an OAuth2
authorization code flow doesn't fit — there's no user, no browser, just a background
job calling our API. The pattern that fits this is HMAC request signing: the client
signs the request using a shared secret and we verify the signature on our side,
similar to how AWS signs its API requests.

The signing process: concatenate the HTTP method, the `Content-MD5` of the request
body, the `Content-Type`, and a UTC timestamp into a canonical string, then compute
`HMAC-SHA256(canonical_string, secret_key)` and put the result in an `Authorization`
header alongside the key ID. Our server reconstructs the same canonical string from
the request, computes the same HMAC with the stored secret for that key ID, and
compares. If they match, the request is authenticated and the body hasn't been tampered
with in transit.

The timestamp in the canonical string is the replay protection: we reject requests
where the timestamp is more than five minutes old. That window has to be wide enough
to tolerate reasonable clock skew between client and server but narrow enough that a
captured request can't be replayed later. Five minutes is a common and defensible
choice. The client library we give partners is about sixty lines — the signing logic
isn't complex to implement, which is part of why I trust it more than schemes that
look complicated on paper.
