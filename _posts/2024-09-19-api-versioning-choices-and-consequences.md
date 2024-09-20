---
layout: post
title: "API versioning choices and their long-term consequences"
date: 2024-09-19
author: marcetux
tags: [api, architecture, rest, versioning, engineering]
---
A consulting client needed to add breaking changes to a public API and asked what
the least painful path was. There's no least-painful path at that point — there's
only choosing which kind of pain. The question should have been asked at design time,
not migration time. So let me write down the choices and what they cost.

URL path versioning (`/v1/`, `/v2/`) is the most common and the most visible. It's
explicit, it's cache-friendly, it's easy to route in a gateway. The cost is that you
maintain multiple versions of the endpoint, and clients have to explicitly upgrade.
For a public-facing API with diverse clients you can't coordinate, this is usually
the right trade — the explicitness is a feature, not a bug. The versions run in
parallel until you can deprecate v1.

Header versioning is cleaner for clients — the URL doesn't change — but harder to
debug and cache, and invisible to clients who don't read the docs carefully. Query
parameter versioning is the worst of both worlds: neither clean nor cache-friendly.
The choice I make for most APIs: URL path versioning for the major version, no
versioning within a major version (additive-only changes), clear deprecation timelines
committed to publicly. The discipline is treating the API as a product with
customers who have migration costs. When you own the cost of a breaking change, you
design differently.
