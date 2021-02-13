---
layout: post
title: "Idempotency in payment API flows"
date: 2021-02-12
author: marcetux
tags: [api, architecture, banking, reliability]
---
A post-mortem from last quarter finally surfaced the root cause of a double-charge
incident: the client retried a timed-out request and the server had no way to tell
it was a retry. The first call had actually succeeded — slowly — but the response
never arrived, so the client tried again and we executed it twice. Classic at-least-
once delivery bug in code that needed exactly-once semantics.

The fix is an idempotency key — a client-generated UUID included with every
mutating request. The server stores the key and the result together; if it sees the
same key again, it returns the stored result without re-executing the operation.
Stripe popularized this pattern and it's the right primitive for any API that moves
money or changes state in ways users expect to happen once. The key window can be
short — 24 hours is typical — because retries happen in minutes, not days.

The awkward part is the database schema. You need to store enough of the response
to replay it faithfully, and the key table needs to survive concurrency: two
simultaneous retries for the same key should both get the right answer and neither
should execute twice. A unique constraint on the key column in Postgres makes the
race condition impossible rather than unlikely, which is the right design. Make the
duplicate harmless by definition rather than by hoping the timing works out.
