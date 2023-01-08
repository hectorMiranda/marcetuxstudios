---
layout: post
title: "Rust error types in practice"
date: 2023-01-07
author: marcetux
tags: [rust, errors, casper, blockchain]
---
Eight months into daily Rust at CasperLabs and the thing I got most wrong early on
was error handling. I reached for `unwrap()` everywhere the way I used to reach for
`try/catch` blocks in C# — slap it in, move on, deal with panics in tests. That
strategy works until you're deploying a smart contract that halts the node and you
have no stack trace worth reading.

The right model is the `?` operator and purpose-built error enums. You define a type
that lists every way this function can fail — `NetworkError`, `InvalidPayload`,
`Timeout` — and each variant carries the context to diagnose it. The `From` trait
wires conversion from upstream error types automatically, so the `?` at each call
site propagates cleanly without boilerplate. The compiler enforces that you've handled
or forwarded every failure mode. That's not ceremony; that's the design doc for your
failure surface.

The library I've settled on is `thiserror` for defining the types — procedural macros
that generate `Display` and `From` from a clean attribute syntax — and `anyhow` for
application code where I want to collect context without designing a hierarchy. Smart
contract code gets the precise enum; the tooling around it gets `anyhow`. Error types
are the thing that tells you how honest a codebase is being about what can go wrong.
