---
layout: post
title: "Rust closures and what they capture"
date: 2022-10-04
author: marcetux
tags: [rust, closures, programming, ownership]
---
I've been using Rust closures for months in iterator chains and async spawns without
thinking hard about the capture semantics, and October was the month a subtle bug
forced me to think about them carefully. The closure capture rules in Rust are the same
ownership system applied to the closure's environment — which is obvious in retrospect
and was not obvious to me when I was fighting the compiler.

A closure captures the variables it uses from its enclosing scope. How it captures them
depends on what it does with them: if it only reads a value, the closure borrows it
immutably; if it mutates it, the closure borrows mutably; if it needs to own it (because
it outlives the scope where the value lives, such as when passed to `tokio::spawn`), it
must take ownership. The `move` keyword forces a closure to take ownership of everything
it captures, which is the pattern you reach for when passing a closure to an async task
that has a `'static` lifetime requirement.

The bug was a closure inside an async block that captured a reference to a local
variable, then got passed to `tokio::spawn`. The spawn requires a `'static` future —
one that owns everything it touches — and the reference violated that. The fix was
`move` on the closure, which cloned the values the closure needed instead of borrowing
them. The lesson: when a closure moves to a new scope — across a thread boundary, into
an async task — it needs to own its environment. `move` is not a workaround; it's the
correct statement of what the closure is doing.
