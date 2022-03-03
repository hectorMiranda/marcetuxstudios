---
layout: post
title: "Rust traits as the contract abstraction layer"
date: 2022-03-02
author: marcetux
tags: [rust, traits, casper, smartcontracts, design]
---
A month into writing Casper contracts, I started feeling the same instinct I had when
writing service layers in C# — the urge to extract an interface. In Rust, the mechanism
is a trait. A trait defines the set of methods a type must implement, and functions that
take `impl Trait` arguments work with any conforming type. That's the abstraction tool,
and for contract development it earns its keep specifically around testability.

The pattern is to define a trait for the storage operations a contract performs —
reading from named keys, writing to named keys, calling into the runtime — and then
implement it twice: once against the real `casper_contract::contract_api` for the
production WASM, and once against an in-memory `HashMap` for tests that run on the
host without spinning up a node. The contract logic lives in functions generic over
the storage trait, which means I can unit-test the logic paths without touching a node
or paying for deploys.

The comparison to C# interfaces isn't perfect — Rust traits have no virtual dispatch
overhead by default, and the monomorphization model generates specialized code for each
concrete type — but the design motivation is identical. Define the boundary. Put the
logic on the inside. Put the platform on the outside. That boundary is where test
seams live, and test seams are why the code is maintainable.
