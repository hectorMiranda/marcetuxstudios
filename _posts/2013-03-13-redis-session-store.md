---
layout: post
title: "Redis as a session store that doesn't embarrass you"
date: 2013-03-13
author: marcetux
tags: [redis, dotnet, aspnet, caching, scalability]
---
The portal runs across two web servers behind a load balancer, and for months we've
had sticky sessions enabled — once you hit web1, the load balancer keeps you there,
because your session is in that server's memory and nowhere else. Sticky sessions work
until web1 reboots, or until web2 is half-idle while web1 is sweating. We fixed it
this week with Redis as the session backing store.

The mechanics are straightforward once Redis is running: swap the session provider in
`web.config` for one that serializes to Redis instead of in-process memory. Every web
server reads and writes the same store. The load balancer can route you anywhere, web1
can restart without dropping sessions, and we can deploy to one server at a time
without logging everyone out mid-deploy. The latency for a Redis read is a millisecond
or two versus nanoseconds for an in-memory dictionary, and I genuinely cannot feel the
difference in a page load.

The other benefit: sessions are now visible. I can `redis-cli keys "session:*"` and see
exactly how many active sessions there are and when they expire. In-process sessions are
a black box; Redis ones are just data you can inspect, expire manually, or query. That
alone — the ability to see and touch the state instead of inferring it — is worth the
migration independent of the load balancing fix.
