---
layout: post
title: "Building a Casper event indexer in async Rust"
date: 2022-06-03
author: marcetux
tags: [casper, rust, async, indexer, blockchain]
---
The event indexer is the piece of infrastructure that makes a dApp usable. The chain
is the truth, but querying the chain for every user request is slow and puts load on
nodes. An indexer subscribes to the node's event stream, transforms the raw events into
application-level data, and writes them to a database that the dApp can query at
application speed. Building this in async Rust has been the most satisfying engineering
work of the year so far.

The architecture is straightforward: a Tokio task per node connection subscribes to the
SSE event stream, parses incoming events (deploy processed, block finalized, fault), and
sends them through a bounded channel to a processor task. The processor enriches the
events — fetching deploy execution results when needed — and writes to PostgreSQL using
`sqlx` in async mode. The bounded channel is the back-pressure mechanism: if the
processor falls behind, the SSE reader applies pressure upstream rather than unbounded
buffering.

What surprised me is how clean the error handling is end-to-end. Every fallible step
returns `Result`, the `?` operator propagates errors up the call chain, and the top-
level task handles reconnection with an exponential backoff loop. The code reads as a
straightforward description of what the system does, and I've had to debug it very
little compared to comparable systems I've built with threads and shared mutable state.
The async model fits I/O fan-out well because the concurrent work is explicit in the
code structure.
