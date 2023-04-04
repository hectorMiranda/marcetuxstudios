---
layout: post
title: "Async Rust and the Tokio runtime"
date: 2023-04-03
author: marcetux
tags: [rust, async, tokio, casper]
---
The Casper client I've been maintaining makes a lot of network calls — deploy
submission, polling, event streaming — and the synchronous approach I started with
has a blocking thread per call smell that I've been meaning to fix. April was the
month I rewrote it on top of Tokio, Rust's async runtime, and the education was
substantial.

Async Rust's model is that `async fn` returns a `Future` — a value that *describes*
a computation rather than running it immediately. You poll futures from an executor,
and `await` suspends the current task while the future is pending, yielding back to
the executor to run other tasks. Tokio is the executor: it manages a thread pool and
dispatches futures across it. The resulting code looks nearly synchronous, but the
actual I/O is non-blocking.

The gotcha I hit: Rust's borrow checker applies to async code in ways that don't
always match your intuition. A reference held across an `await` point must be `Send`
— safe to pass between threads — because the executor might resume the task on a
different thread. The compiler error message is clear, but the fix ("restructure
the function so the reference drops before the await") took me a while to internalize.
Now that I have it, the mental model is clean: `await` = thread handoff opportunity =
nothing borrowed at that point that isn't threadsafe.
