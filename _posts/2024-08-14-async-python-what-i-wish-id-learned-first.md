---
layout: post
title: "Async Python and what I wish I had learned first"
date: 2024-08-14
author: marcetux
tags: [python, async, concurrency, architecture, patterns]
---
I came to Python async from a C# async/await background, which is the right place
to come from conceptually and the wrong place to come from in terms of expectations.
The models look the same — `async def`, `await`, coroutines — and that similarity
is deceptive because the runtime models are different in ways that matter for
everything above toy programs.

The thing I wish someone had told me first: in Python, the event loop is explicit.
`asyncio.run()` creates and runs one. If you're in a framework like FastAPI or
aiohttp, the framework runs the loop for you. If you're writing a script, you run
it yourself. In C#, `Task.Run` and the thread pool handle this implicitly. The
explicitness isn't a flaw — it makes the model clearer once you see it — but it
means you need to know which context you're in. Calling `asyncio.run()` inside a
running event loop is an error, not a warning.

The second thing: `asyncio` is single-threaded concurrency, not parallelism. Awaiting
an I/O operation yields control so other coroutines can run. CPU-bound work in an
async function blocks the event loop until it finishes. The fix for CPU-bound work
in an async context is `asyncio.to_thread()` (or `loop.run_in_executor()`), which
runs it in a thread pool without blocking the loop. Know what kind of concurrency
you're buying before you add `async` to a function. It's not free, and it's not
magic.
