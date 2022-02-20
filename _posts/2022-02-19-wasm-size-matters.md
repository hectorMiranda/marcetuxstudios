---
layout: post
title: "WASM binary size and why it matters for on-chain contracts"
date: 2022-02-19
author: marcetux
tags: [wasm, rust, casper, optimization, smartcontracts]
---
Every Casper deploy has a payment amount — motes you're willing to spend on execution —
and that cost scales with the complexity of what you're deploying. Contract size is
part of that: you pay for the WASM bytes that go on-chain, and a bloated binary is just
money left on the table. The default Rust release build for even a trivial contract
can produce an embarrassingly large WASM file if you're not careful.

The main culprits are standard library panicking infrastructure and debug symbols
bleeding into the release binary. Using `opt-level = 'z'` (optimize for size, not
speed) in the `Cargo.toml` release profile is the first knob. Adding `lto = true`
and `codegen-units = 1` lets the linker do more cross-crate dead-code elimination.
Then `wasm-opt` from the Binaryen project can shrink the result another twenty to
thirty percent through its own passes. A contract that started at 200KB can land at
under 50KB with these settings, which is not cosmetic — it's real cost reduction on
every deploy.

The constraint is clarifying. Smart contract development is one of the few contexts
left where you think carefully about the size of your binary, and that discipline
carries over to thinking carefully about what the contract actually does. Every import,
every dependency, every codepath that ends up in the binary costs something. Make it
count.
