---
layout: post
title: "Rust async and the mental model that finally helped"
date: 2024-03-11
author: marcetux
tags: [rust, async, concurrency, systems]
---
I've been writing Rust since CasperLabs and async Rust was the part that took the
longest to actually click. The syntax is fine. The mental model is not the thing the
syntax suggests, and that mismatch is where people get lost. An async function in
Rust doesn't run when you call it — it returns a Future, and nothing happens until
something polls that Future. The runtime is the thing that polls.

In tokio, the executor polls futures and drives them to completion. `await` is the
point where a future says "I'm not ready; come back later" and yields control so
the executor can poll something else. That's the cooperative yield point. If you
never await, you never yield, and a single async task can starve the whole thread.
I've seen this in practice: a tight loop without an await point that blocks the
tokio runtime thread. Moving the CPU work to `spawn_blocking` fixes it because now
it runs on a dedicated thread pool and the async threads stay clear.

The thing that finally made it concrete: I thought of each Future as a state machine
that the executor drives forward one poll at a time. When you write `async fn`, the
compiler generates that state machine. The state at each `await` is one of the
states. Reading the generated state machine — there's a `--emit=mir` flag that gets
you close — is tedious but illuminating. Understanding the machine is easier than
fighting the abstractions when something goes wrong.
