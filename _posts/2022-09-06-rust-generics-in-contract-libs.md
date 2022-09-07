---
layout: post
title: "Rust generics for shared contract library code"
date: 2022-09-06
author: marcetux
tags: [rust, generics, smartcontracts, casper, design]
---
By September we had three related contracts sharing enough logic that copy-paste was
visibly wrong. The shared logic — argument parsing helpers, access-control checks,
common error types — needed to live in a crate the contracts could depend on. Extracting
it forced me to think seriously about the generic boundaries, which is where I learned
the most.

Rust generics are monomorphized: the compiler generates a concrete version of a generic
function for each set of type arguments it's called with. There's no virtual dispatch,
no boxing, no runtime type information — just specialized code, compiled to the most
efficient form for each use. For contract library code where the WASM binary size has
to stay manageable, that means thinking about which generic functions will be
instantiated and whether the cost of multiple specializations is worth the abstraction.

The contract library crate settled on two strategies. For code that doesn't depend on
the concrete type — error mapping, argument extraction helpers — we used free functions
and traits, no generics. For code that does vary by type — storage wrapper functions
that read and write specific `CLType`-implementing types — we used generic functions
bounded by the storage traits. The resulting crate is small, the contracts that use it
stay small, and we've extracted the argument-parsing bugs once instead of once per
contract. A shared library is its own kind of contract: define it clearly, break it
rarely, version it deliberately.
