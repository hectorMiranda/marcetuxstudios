---
layout: post
title: "gRPC for inter-service calls in .NET and why JSON feels wasteful now"
date: 2020-08-03
author: marcetux
tags: [dotnet, grpc, architecture, performance]
---
The payment flow inside the cluster makes seven inter-service calls in the happy path.
Every one of them serializes a JSON payload over HTTP/1.1. I measured it for the first
time last month and the serialization overhead — not the network, the serialization —
is between 12 and 18% of total request time on the slowest calls. Not catastrophic,
but measurable, and gRPC has been on my list to try since the .NET Core 3.0 support
landed.

gRPC's binary framing via Protobuf is roughly 5x smaller than the equivalent JSON
and materially faster to serialize. The HTTP/2 transport means multiplexed calls over
a single connection instead of connection-per-request. The contract is a `.proto`
file; the toolchain generates strongly-typed client and server stubs. The first thing
you notice when working with generated types: the service interface is checked at
compile time. If the server changes a field name, the client build fails. That's the
kind of contract enforcement that OpenAPI gives you only if you run a generator step
and remember to re-run it.

The pilot is the batch-pricing service — high call volume, stable schema, internal-
only traffic. The migration was a day's work: write the proto, swap the HTTP client
for the generated gRPC client, update the registration in DI. The latency improvement
on that path is around 20%. The bigger gain is the contract discipline. gRPC doesn't
let you drift; JSON over HTTP is polite about it.
