---
layout: post
title: "Bitcoin breaks a thousand dollars and I have no idea what happens next"
date: 2013-12-03
author: marcetux
tags: [bitcoin, cdn, caching, distributed-systems]
---
Bitcoin crossed $1,000 last night. I watched it happen on a price ticker that was
visibly straining under request volume. The ticker itself is a case study: it was
probably not designed to be the focal point of every new Bitcoin observer arriving
simultaneously, and the response time showed it.

The engineering angle I can't stop thinking about: distributed digital scarcity is
a solved computer science problem at the protocol level — the blockchain is the
authority on who owns what, and the math for that is not particularly controversial.
The hard parts are exactly the distributed systems problems engineers deal with all
day: double-spend prevention is a consensus problem, wallet synchronization is an
eventual consistency problem, exchange matching is a latency-critical ordering problem.
The currency aspect is interesting; the infrastructure is familiar.

What I don't know is whether $1,000 is a price that reflects anything durable. The
traffic patterns on the CDN suggest there are a lot of new observers arriving this week,
which is classic bubble signaling — asset prices go up, new participants arrive, prices
go up more, not because the fundamental value changed but because more people are
watching. I've been wrong about this before; the price was $100 in April and I assumed
it was going to correct. It might correct from here. It might not. The blockchain is
interesting; the price speculation is outside my ability to reason about usefully.
