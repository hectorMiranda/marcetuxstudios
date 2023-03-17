---
layout: post
title: "Rust lifetimes, a working mental model"
date: 2023-03-16
author: marcetux
tags: [rust, lifetimes, memory, casper]
---
A year in, I finally have a mental model for Rust lifetimes that doesn't break down
the moment a function takes two references. The thing that clicked: a lifetime is not
a duration in time, it's the region of code where a reference is valid. When you write
`'a`, you're naming a relationship between a reference and the data it points to, so
the compiler can verify the reference can't outlive the data.

The rule for functions is "the output reference can live as long as the shortest input
reference that could have produced it." Most of the time the compiler figures this out
with lifetime elision and you don't write annotations. You write them when it can't:
when a function returns a reference and there are multiple input references that could
be the source. You're telling the compiler which input this output is tied to.

Where this paid off at work: a caching layer I wrote over the contract client returned
a reference into an internal `HashMap`, and a caller was holding that reference while
also calling a method that could modify the map. The compiler refused. That was not a
false positive — that was a use-after-mutation bug caught before testing. I've written
enough C# to know what that bug looks like at runtime. The annotation ceremony is a
small price for catching it at compile time.
