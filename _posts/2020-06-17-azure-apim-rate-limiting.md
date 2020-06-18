---
layout: post
title: "Rate limiting at the API gateway before it reaches your service"
date: 2020-06-17
author: marcetux
tags: [azure, api, architecture, security]
---
A partner integration team hit one of our internal APIs with a misconfigured retry
loop — five hundred calls per minute for forty minutes before someone noticed.
The API didn't go down, but it used resources it shouldn't have, and the Splunk
alert for "unusual traffic pattern" fired six minutes in, which is six minutes
longer than it should take. The right answer is a rate limit at the gateway that
makes the problem self-limiting.

Azure API Management has quota and rate-limit policies in its XML policy language.
A rate limit of, say, 60 calls per minute per subscription key fires a 429 before
the request ever reaches the backend service. The partner gets a clear signal —
429 with a `Retry-After` header — rather than a slow degradation or an opaque
timeout. From the service's perspective, the burst simply doesn't exist.

The subtlety is that quotas and rate limits serve different purposes. A rate limit
is per-window — 60 per minute, sliding or fixed — and protects against spikes. A
quota is per-period — 10,000 per month — and is a commercial/tiering control. Both
are configured in APIM, both are enforced before your backend. The partner
integration runbook now includes APIM subscription key setup and rate limit
expectations explicitly. No more assuming good behavior from misconfigured clients.
