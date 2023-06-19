---
layout: post
title: "Rust iterator patterns worth knowing"
date: 2023-06-19
author: marcetux
tags: [rust, iterators, casper, patterns]
---
The Rust standard library's iterator trait is the thing I find myself explaining most
to developers coming from other languages, because it's both more capable and more
restrictive than what they expect. In C# you have LINQ; in Python you have
comprehensions and generators. Rust iterators are lazy, owned, and composable in a
way that interacts with the ownership system in ways that surprised me.

The pattern I use most: `filter_map`. It's `filter` and `map` in one pass — return
`None` for elements you want to skip, `Some(transformed)` for elements you want to
keep. Parsing a list of raw RPC responses where some might be errors: `.filter_map(|r| r.ok())` drops the errors and unwraps the successes in one expression. No `if let`
block, no separate filter step, no allocation of intermediates.

The restriction worth naming: once you've called `.into_iter()` on a collection, the
collection is consumed. You can't iterate the same collection twice unless you clone it
or use `.iter()` (which borrows) instead. Coming from C# where you can call
`foreach` on the same `IEnumerable` repeatedly, this feels like a limitation until you
realize why: iterating by value and iterating by reference are genuinely different
operations with different ownership semantics. Once you've internalized that, the
`.iter()` / `.iter_mut()` / `.into_iter()` distinction becomes a useful API rather than
an obstacle.
