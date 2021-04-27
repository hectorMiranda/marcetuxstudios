---
layout: post
title: "Rate limiting patterns beyond token buckets"
date: 2021-04-26
author: marcetux
tags: [api, architecture, reliability, azure]
---
I implemented rate limiting on the public-facing loan-inquiry endpoint and the
naive version — a fixed token bucket per API key — shipped correctly and then
immediately generated a support ticket. A legitimate batch job was bursting at the
start of its run and hitting the limit before it had processed anything meaningful.
The limit was protecting the service; it was also blocking a real use case.

Sliding window is better for most cases than fixed window or fixed token bucket,
but the client's complaint pointed at a different problem: the bucket was per-key
and per-minute with no allowance for how traffic was spread across a day. A batch
job that uses 1,000 requests in the first minute of its run and zero requests for
the next twenty-three hours is not the client you're trying to throttle. Composite
limits — requests-per-minute with a separate requests-per-day ceiling — model the
actual intent better. Cap the burst, give the patient client room.

The second fix was a `Retry-After` header on every 429 response, with the exact
epoch time when the window resets. A client that knows when to retry doesn't need
exponential backoff guessing — it can sleep exactly as long as needed and come back
precisely when the window opens. Good API design at the error surface is just
as important as at the happy path. The client that gets a useful 429 is not a
frustrated client; it's a client you gave enough information to behave correctly.
