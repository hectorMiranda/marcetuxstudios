---
layout: post
title: "Rust ownership in practice, not in theory"
date: 2022-01-07
author: marcetux
tags: [rust, programming, ownership, memory]
---
I've read the ownership chapter in the Book twice and understood it fine in the
abstract. Actually writing Rust in anger is a different thing. The borrow checker is
a patient, humorless colleague who tells you every place your assumptions about object
lifetime are wrong, and the first week at CasperLabs was me arguing with it about
functions that try to own the same value from two places at once.

The thing that clicked is that ownership isn't garbage collection with more paperwork —
it's a system that forces you to decide, at the type level, who is responsible for a
value at every point in the program. You can **move** a value to a new owner, **borrow**
it immutably to any number of readers, or **borrow mutably** to exactly one writer. Those
three choices are all you get, and the compiler verifies you stayed in bounds. The
payoff is no garbage collector, no use-after-free, no data race — the whole class of
bugs that costs weeks in C++ just doesn't compile in Rust.

What I'm still calibrating is the ergonomics. The instinct to reach for a shared
reference where a clone is the right answer, or to avoid a clone where a reference
creates a lifetime that makes the code unnecessarily tangled — that judgment takes
repetition. The WASM contract work is giving me plenty of repetition. The borrow
checker is not unreasonable; it's unambiguous, which is a different thing.
