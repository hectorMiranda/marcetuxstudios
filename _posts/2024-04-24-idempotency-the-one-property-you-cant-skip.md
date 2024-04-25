---
layout: post
title: "Idempotency is the one property you cannot skip"
date: 2024-04-24
author: marcetux
tags: [architecture, api, distributed-systems, engineering, patterns]
---
A consulting client had a payment integration that was double-charging a small
percentage of customers. Not a high percentage — one or two a week — but a payment
double-charge is a customer-trust event that punches above its frequency. The root
cause was predictable: the API call succeeded, the network response didn't make it
back, the client retried, and the server processed it again. A classical distributed
systems problem. The fix was also classical: idempotency keys.

An idempotency key is a client-generated identifier for the intent of a request.
The server stores a mapping from key to result. If the same key arrives twice, the
server returns the stored result instead of processing again. The client can retry
freely; only one charge happens. The storage cost is bounded (key expires after
some TTL), the logic is concentrated in one place, and the retry behavior at the
client requires zero change — it retries as before, now safely.

The harder conversation is with clients who resist the extra storage. "We can just
check if the record already exists." That works for simple creates; it doesn't work
when the result is complex, when the original processing had side effects, or when
partial failures leave the record in an intermediate state. The idempotency key
stores the *result* so you can replay it exactly. This is worth the Redis key. In
twelve years I have never seen a distributed mutation that couldn't be improved by
idempotency. Some are fine without it. None are hurt by it.
