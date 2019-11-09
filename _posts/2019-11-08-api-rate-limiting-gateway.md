---
layout: post
title: "Rate limiting at the API gateway layer"
date: 2019-11-08
author: marcetux
tags: [api, gateway, security, architecture, banking]
---
The load test from last week made rate limiting urgent rather than theoretical. If a misbehaving client or a misconfigured integration partner can send three thousand payment requests per minute — our staging breaking point — then the gateway needs to say no before those requests reach the service. Rate limiting protects our resources and also protects the client from itself when a retry loop goes wrong.

We implemented rate limiting at the Azure API Management gateway with policies scoped by subscription key. Each integration partner authenticates with a subscription key; the policy allows 100 requests per minute per key by default, with the limit configurable per partner based on their contractual volume. Exceeding the limit returns 429 with a `Retry-After` header containing the number of seconds before the limit resets. The client has everything it needs to back off gracefully.

The nuance is distinguishing rate-limiting from throttling, which APIM also supports. Rate limiting says "you may not exceed N requests per window" and enforces it hard. Throttling in APIM is a softer control that queues excess requests up to a buffer, smoothing bursts at the cost of latency. For payment initiation — where a 200ms extra latency matters less than an unexpected 429 — throttling with a moderate queue buffer is actually the better answer for moderate bursts. Rate limiting is for sustained overconsumption. Having both controls and understanding what each one does is the thing I had to work through with the APIM policy documentation before the first deploy.
