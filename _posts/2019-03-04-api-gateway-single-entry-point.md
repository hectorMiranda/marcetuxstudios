---
layout: post
title: "The API gateway as the single honest entry point"
date: 2019-03-04
author: marcetux
tags: [architecture, api, gateway, security, banking]
---
We had a proliferation problem: every internal service had grown its own public endpoint, its own TLS termination, its own auth token validation logic — each slightly different, each a separate surface for a misconfiguration to hide in. The API gateway project was partly about routing and partly about agreeing that there is one place where network trust terminates and authentication happens, and everything behind it is private.

The gateway does TLS termination, JWT validation, rate limiting, and request logging before a byte hits any backend service. What the backend sees is an already-authenticated request with the caller's claims in a header the gateway stamped and signed. The backend trusts that header because it trusts the gateway's mTLS client certificate — which is the point where the two patterns from February connect. The gateway is an internal CA client, and the backends only accept calls from certificate holders the CA knows about.

The hard part was convincing teams that giving up their public endpoints was a gain, not a loss. Every team had a reason their service was "different." It almost never was. The one legitimate exception: a service that needed to receive callbacks from a third-party payment network had to stay directly addressable. We carved that out explicitly, applied extra scrutiny to it, and documented the exception in the architecture record. Exceptions you name and justify are fine. Exceptions that accumulate because nobody said no to the first five are the architecture.
