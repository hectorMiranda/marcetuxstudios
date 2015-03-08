---
layout: post
title: "Docker Compose networking between services"
date: 2015-03-07
author: marcetux
tags: [docker, devops, networking, rails]
---
The compose file grew a Redis container this month — the Rails app uses it for session
storage and the rate-limiter — and the first question was how the Rails container
addresses the Redis container. In Docker Compose, the answer is the service name: if
your Redis service is named `redis`, the Rails app connects to `redis:6379` and Compose
resolves it via the internal network it creates automatically.

The magic is less magic when you read it: Compose creates a bridge network for the app,
adds each container to it, and registers each container's service name as a DNS entry
on that network. So `redis` resolves to the Redis container's internal IP. No IPs to
hard-code, no `/etc/hosts` entries to manage. The environment variable you override to
point the app at the right host is `REDIS_URL=redis://redis:6379`, and the same var
works in staging with a real Redis instance because the service name just happens to
match the hostname there.

What this replaced was a `.env` file full of `127.0.0.1:6379` entries that worked on
the developer's machine and nowhere else. The compose networking model makes "the
connection string is a hostname the environment resolves" the natural idiom. Environment-
variable configuration was already the right call; now the local dev environment honors
the same convention as every other environment.
