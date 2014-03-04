---
layout: post
title: "The microservices word is everywhere now"
date: 2014-03-03
author: marcetux
tags: [architecture, microservices, soa, backend, devops]
---
Martin Fowler and James Lewis published the microservices piece this month and now
the word is in every conversation. The idea isn't new — "small services that do one
thing" is just SOA with a better marketing budget — but the article finally gave the
concept a name precise enough that people can argue about the same thing. That's
actually useful.

The core of it is that each service owns its own data store and can be deployed
independently. No shared database between services; if service A needs data from service
B, it asks via API. Independently deployable means you can ship the billing service on
a Tuesday without touching the reporting service, which is the part that makes
DevOps at scale tractable. At EdgeCast, the portals codebase is a monolith, but the
back-end systems it calls are already partitioned along roughly these lines — bandwidth
ingestion, customer management, billing, report generation each live in separate
codebases with their own deployment pipelines.

What I'm skeptical of is the distributed systems complexity that comes with it. Once
you split a relational join across a network call, you've traded a cheap local operation
for a timeout, a retry policy, and an eventual consistency question. The monolith's
problems are visible in a SQL query plan; the microservices problems are visible in
production at 2 AM. Worth understanding the trade before you make it.
