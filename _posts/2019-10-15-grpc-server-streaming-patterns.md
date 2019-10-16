---
layout: post
title: "gRPC server streaming for real-time feeds"
date: 2019-10-15
author: marcetux
tags: [grpc, dotnet, streaming, architecture, realtime]
---
The account balance aggregator from June has a sibling use case: a risk dashboard that needs a continuous stream of flagged transaction events as they're detected, rather than polling an endpoint. The unary gRPC request-response model would mean either polling at high frequency (wasting bandwidth and creating load spikes) or long-polling (complex to implement and fragile). Server streaming is the right model, and I've been using it in production for two months now.

In a gRPC server streaming RPC, the client sends one request and the server responds with a stream of messages. The proto definition declares `returns (stream FlaggedTransactionEvent)` instead of `returns (FlaggedTransactionEvent)`. The server method receives `IServerStreamWriter<T>` and writes to it until the stream is complete or the client cancels. On the client side, the generated code provides `ResponseStream.MoveNext(ct)` for pulling the next item — or, in the newer async streaming support in C# 8, you can use `await foreach` over `GetAsyncEnumerator`.

The operational nuance: gRPC streams over HTTP/2 are long-lived connections. HTTP/2 has built-in flow control, but the application layer also needs to handle backpressure — if the risk dashboard client is slower than the event rate, the server-side write buffer fills up. We added a write timeout and a circuit-breaker on the streaming write: if a write blocks for more than two seconds, we close the stream and expect the client to reconnect. The client has retry logic with exponential backoff. Streams break; reconnect gracefully and don't pretend you didn't miss events during the gap.
