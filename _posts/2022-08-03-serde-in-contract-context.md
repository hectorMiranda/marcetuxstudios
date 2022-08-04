---
layout: post
title: "serde for Casper contract serialization, where it fits and where it does not"
date: 2022-08-03
author: marcetux
tags: [rust, serde, serialization, casper, smartcontracts]
---
`serde` is Rust's serialization framework and it's one of the things the ecosystem gets
exactly right. The `#[derive(Serialize, Deserialize)]` macros do what they say, the
format pluggability means you use the same derive for JSON, MessagePack, CBOR, or
anything that has a serde adapter, and the compile-time code generation means you pay
no runtime overhead you didn't ask for. In off-chain Rust code — the indexer, the
tooling — I use it everywhere.

In on-chain contract code, the picture is different. Casper contracts communicate
through the `CLValue` type system — Casper's own binary encoding for deploy arguments
and stored values — not through arbitrary serde formats. You can use serde inside
a contract for intermediate processing, but anything that goes into named keys or
comes in as deploy arguments has to go through `CLType` encoding. The `casper_contract`
crate provides `FromBytes` and `ToBytes` traits, which are the Casper equivalent of
serde for on-chain data.

The discipline that follows from this is keeping the serialization boundary explicit.
A struct that lives only in contract memory during execution can derive serde; a struct
that needs to be stored on-chain needs `CLTyped + FromBytes + ToBytes`. Mixing up which
world a type lives in is a common source of confusion for Casper newcomers, and the
compiler won't always catch it because both trait families can coexist on the same type.
Draw the boundary in the code organization, not just in your head.
