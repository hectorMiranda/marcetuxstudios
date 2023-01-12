---
layout: post
title: "WASM components and the next layer of portability"
date: 2023-01-11
author: marcetux
tags: [wasm, rust, architecture, blockchain]
---
Working on a Casper smart contract means shipping Rust compiled to WebAssembly — that
part I've been doing for months. But the Component Model proposal changes what WASM can
actually be, and I've been reading the specs carefully because this matters beyond
blockchain.

Plain WASM modules share nothing: you get a flat byte array of memory and a list of
imported functions with numeric types. The Component Model adds an interface layer on
top, defined in WIT (WebAssembly Interface Types), that lets components describe their
API in actual types — strings, records, variants — and link to each other without
caring what language each was written in. A Rust component exports a typed interface;
a Go component imports it; neither knows what the other is. That's a different
proposition than "we can run code in the browser."

For smart contracts this matters because the tooling ecosystem is fragmented by
chain. For everything else it matters because "small, sandboxed, typed, link-anywhere"
describes a plugin or extension model that doesn't require shipping a whole runtime.
The component model is still draft-spec territory and the toolchain is moving fast,
but I'd rather learn it early. Bytes that respect a type boundary are almost always
easier to reason about than bytes that don't.
