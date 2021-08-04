---
layout: post
title: "Message queue backpressure and what happens without it"
date: 2021-08-03
author: marcetux
tags: [architecture, messaging, reliability, azure]
---
A batch processing job that consumes from an Azure Service Bus queue ran into a
wall last month: the consumer was slower than the producer and the queue depth grew
until the processing lag was measured in hours. The job itself was fine in isolation.
Under real load the latency was invisible until it was catastrophic. No one had
modeled what happened if the consumer fell behind, because the queue is supposed
to handle that — and it does, right up until the latency SLA makes the queue depth
meaningless.

Backpressure is the mechanism that slows a producer when a consumer can't keep up.
Azure Service Bus doesn't push messages at you faster than you pull them, which is
the basic backpressure guarantee. The problem was the consumer was spinning up too
many parallel processing tasks — pulling a hundred messages, processing them in a
`Parallel.ForEach` with no concurrency limit — and the database downstream was the
actual bottleneck. The queue depth grew because the database couldn't handle the
parallelism the consumer was generating.

The fix: explicit concurrency control on the consumer side. `SemaphoreSlim` around
the message processing loop, limiting concurrent in-flight messages to a number
the database can serve without queuing. The queue depth stabilized within a minute
of deployment. Backpressure is not something the infrastructure does for you
automatically; it's something you design into the processing chain. Every buffer in
the system has a capacity, and the question is whether you find that capacity limit
in the design or in production at 2 a.m.
