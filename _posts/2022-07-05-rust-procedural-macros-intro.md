---
layout: post
title: "Rust procedural macros and where they earn their complexity"
date: 2022-07-05
author: marcetux
tags: [rust, macros, metaprogramming, programming]
---
The `#[derive(Debug, Clone, Serialize)]` annotations on Rust structs are procedural
macros — they run at compile time, take a token stream representing your type, and
generate code that gets compiled alongside it. I'd been using them for months without
thinking much about the mechanism. This month I read through how `serde`'s derive macro
generates serialization code, and the internal complexity is considerable, but the
external simplicity is the point.

Writing your own procedural macro requires the `proc-macro` crate and the `syn` and
`quote` libraries — `syn` for parsing the input token stream into a usable AST, `quote`
for generating output token streams. The pattern is: parse the input into structured
data about the type (fields, attributes, generic parameters), then use `quote!` to
emit the generated impl block. The macro crate lives in a separate workspace member
because proc-macro crates have to be compiled for the host architecture, not the target.

Where I've seen them earn the complexity at work: the `#[casper_contract]` attribute
macro handles entry point registration boilerplate automatically, so contract code
doesn't have to manually declare and register each entry point name. The macro reads
your function signatures and emits the glue code. That's a case where the macro
removes entire categories of bugs — mismatched entry point name strings between the
Rust function and the registration call — and the author of the contract never sees
the generated code. The abstraction is doing real work.
