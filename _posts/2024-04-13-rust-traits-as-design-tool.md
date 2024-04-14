---
layout: post
title: "Rust traits as a design tool not just a type mechanism"
date: 2024-04-13
author: marcetux
tags: [rust, traits, design, architecture, systems]
---
I spent a few years in Rust at CasperLabs before I could say I was using traits
rather than fighting them. The fighting phase is where you discover that traits are
not interfaces. They look like interfaces — a named contract, a set of methods —
but they behave differently at the call site in ways that matter for design.

The key insight is that a trait can be implemented for any type, including types
you didn't write, in any crate that can see both the trait and the type. That's the
"orphan rule" constraint, but within it, this means the extension surface is huge.
I've been using this to write testable seams into code that would otherwise require
mocking frameworks: define a trait for the behavior, implement it for the real type,
implement it for a test double, pass the trait bound to the function under test. The
function doesn't know or care which implementation it gets.

Where I kept tripping: using trait objects (`dyn Trait`) when I should have used
generics with trait bounds, and vice versa. Generics monomorphize — the compiler
generates a concrete version for each type, fast but larger binary. Trait objects
use dynamic dispatch — one code path, runtime overhead. The rule I landed on:
generics when the concrete types are known at compile time, trait objects when you
need a collection of mixed types or a plugin boundary you'll extend at runtime.
Knowing which you want is a design decision, not a language detail.
