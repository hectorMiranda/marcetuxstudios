---
layout: post
title: "gRPC streaming and the Casper event stream"
date: 2022-02-10
author: marcetux
tags: [grpc, casper, blockchain, streaming]
---
The Casper node exposes two interfaces: the JSON-RPC endpoint for submitting deploys
and querying state, and a separate SSE (server-sent events) endpoint that streams
block and deploy events. Internally at CasperLabs the tooling we're building talks to
nodes over gRPC, and getting my head around bidirectional gRPC streaming this month was
the main technical puzzle.

gRPC streaming is what happens when you want to go beyond the one-request-one-response
model that plain gRPC shares with REST. Server-streaming lets the server send a
sequence of responses to a single client request — exactly what you want for subscribing
to a stream of new blocks as they're finalized. The client sends one subscribe message,
holds the connection open, and receives a stream of `FinalizedBlock` events until it
disconnects or the server closes the stream. Compared to polling the JSON-RPC endpoint
for new blocks, this is lower latency and much cleaner code.

The Rust implementation uses `tonic` — the de-facto gRPC library for the ecosystem —
and the protobuf definitions live alongside the node code. What I appreciate about the
proto-first approach is that the schema is the contract, versioned and checked in, not
a wiki page someone forgot to update. The streaming types fall out of the generated
code: `impl Stream<Item = Result<FinalizedBlock, Status>>` is exactly what it sounds
like. The model is cleaner than the SignalR hubs I was using three years ago, and I
wish I'd been doing proto-first longer.
