---
layout: post
title: "Rust to WebAssembly for the browser's number-crunching"
date: 2021-09-07
author: marcetux
tags: [rust, wasm, frontend, performance]
---
The internal pricing tool has a Monte Carlo simulation that was running in C# on
the server — a request-per-simulation model that put server CPU under load during
peak usage and introduced noticeable latency. The calculation itself is embarrassingly
parallel and pure: no I/O, no shared state, just numbers in, numbers out. That
profile is exactly what WebAssembly is good at, and after the Rust exploration from
earlier in the year I had the prerequisite skills to try it.

I compiled a Rust implementation of the simulation to WASM using `wasm-pack`. The
integration with the browser JavaScript is surprisingly ergonomic: `wasm-pack build`
produces an npm package that you import like any other module, and the boundary
between JS and WASM is annotated with `#[wasm_bindgen]` in the Rust code. Passing
simple arrays across the boundary is efficient; complex objects require serialization.
For this use case — slice of floats in, slice of floats out — the boundary cost is
negligible.

The result: the simulation runs in the browser, ~4x faster than the JavaScript
equivalent I benchmarked against, the server is out of the loop for this
calculation, and the code is Rust, which means the borrow checker and the type
system kept me honest during development in ways that JavaScript doesn't. The
compiled WASM module is 180 KB. The page loads it once, caches it, and runs it
repeatedly. It's a narrow use case, but it's a real one, and it hardened my sense
that the Rust+WASM combination is worth the investment.
