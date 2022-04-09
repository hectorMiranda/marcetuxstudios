---
layout: post
title: "Rust iterators and why I stopped writing for loops"
date: 2022-04-08
author: marcetux
tags: [rust, iterators, functional, programming]
---
The thing that made LINQ click for me back in C# was realizing that the query was a
description of what you wanted, not the steps to get it — the runtime handled the steps.
Rust iterators are the same insight, executed differently. An iterator over a collection
is lazy: nothing executes until you call a consuming method like `collect()`, `sum()`,
or `for_each()`. The chain of `map()`, `filter()`, `flat_map()` calls is a pipeline
description, and the compiler fuses them into tight loops — no intermediate allocations
for each transformation step.

For contract code where allocation is gas, this matters in a concrete way. A chain
of three `map` and `filter` calls on a vector doesn't create two temporary vectors;
it walks the original data once, transforming and filtering inline, and produces
exactly one output. The zero-cost abstractions claim in Rust is most legible in the
iterator implementation: the abstraction compiles away entirely.

What I've noticed in my own code is that once I internalized the iterator model the
`for` loop mostly disappeared from anything that's transforming data. A `for` loop is
an explicit step-by-step, and it requires the reader to trace the loop to understand
what came out. `vec.iter().filter(|x| x.is_valid()).map(|x| x.value).collect()` says
what it does in the structure of the code, not in the comments above it. The reader
spends less energy reconstructing intent.
