---
layout: post
title: "Rust async, tokio, and the executor mental model"
date: 2022-05-14
author: marcetux
tags: [rust, async, tokio, concurrency]
---
The off-chain tooling at CasperLabs — the infrastructure that talks to nodes, indexes
events, serves query APIs — is async Rust using Tokio. The on-chain contracts are
synchronous (no async in the WASM runtime, by design), so this is the first place I've
had to seriously engage with Rust's async model, and the mental model required is
different from C#'s async/await even though the surface syntax is similar.

The key difference is that Rust async functions return a `Future` that is inert until
driven by an executor. In C#, `await` hands the task to the runtime's thread pool and
you don't think much more about it. In Rust, the future does nothing — produces no
progress — unless something is polling it. That something is the executor, and Tokio is
the most common one: an async runtime that schedules futures on a thread pool, wakes
them when I/O is ready, and drives them to completion. The `#[tokio::main]` attribute
on your `main` function sets up the runtime and starts the polling loop.

Once the executor model clicks, the rest follows. `tokio::spawn` launches a future
concurrently, like a Task in C#. `join!` waits for multiple futures concurrently.
`select!` races multiple futures and takes the first to complete. The primitives map
well to the underlying concepts; the unfamiliarity is in understanding that the runtime
doesn't come for free — you are explicitly opting into it, and that opt-in is a feature.
