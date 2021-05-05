---
layout: post
title: "Rust, a first serious look"
date: 2021-05-04
author: marcetux
tags: [rust, languages, systems, learning]
---
I've picked up and put down Rust three times since 2018, never getting past the
borrow checker before a work deadline pulled me back to C#. This month I made
myself actually finish the Rustlings exercises and build something small that
compiles and runs: a command-line tool that reads a CSV of temperature samples and
emits per-sensor statistics. Trivial problem, on purpose. I wanted to fight the
language, not the domain.

The borrow checker is not as hostile as I remembered once I stopped trying to write
C# with different syntax. The model is: every value has exactly one owner; you can
lend a reference for reading (many readers are fine) or for mutation (one writer,
no readers, simultaneously). The compiler enforces this at compile time, which means
there is no garbage collector and there are no data races — not "we test for races"
but "the compiler will not compile code that has this class of problem." Coming from
languages where these are runtime concerns, the shift in where the work happens is
significant.

What's pulling me toward it beyond the safety story is the embedded/WebAssembly
angle. Rust compiles to WASM with less friction than C and without the runtime weight
of .NET. For the ESP32 work — currently all Arduino C++ — there's a `no_std` Rust
ecosystem growing that targets bare-metal embedded. I'm not switching the home
projects to Rust this month, but I'm paying closer attention than I was last year.
