---
layout: post
title: "Rust traits are not interfaces but they're related"
date: 2021-11-22
author: marcetux
tags: [rust, languages, design, learning]
---
Traits in Rust are the thing C# engineers reach for the interface analogy to
explain, and the analogy is useful as far as it goes and misleading past that.
Both are a named set of capabilities that a type can implement. The difference is
what the compiler knows at compile time and what you defer to runtime.

A C# interface is a runtime contract: a variable of interface type is a reference
that, at runtime, could be any implementing type. The dispatch to the right
implementation is dynamic. Rust's trait objects — `dyn Trait` — work the same way.
But Rust also has **generics bounded by traits**, where the concrete type is known
at compile time and the implementation is monomorphized: one compiled function per
concrete type. No virtual dispatch, no heap allocation, no indirection. The compiler
sees the complete concrete type at the call site.

The implication is that "implement this trait" in Rust can mean two different things
depending on how the caller uses the type. A function `fn process<T: Serialize>(item: T)`
works with any type that implements `Serialize`, and the compiler generates a
separate `process` for each concrete type used. A function `fn process(item: &dyn Serialize)`
works with any type via dynamic dispatch. Both are valid; the choice is between
compile-time type knowledge and runtime flexibility. In C# you only have the
runtime version for interfaces. That additional axis — static vs. dynamic dispatch
as a call-site choice — is one of the things that makes the Rust type system feel
more precise than C#'s.
