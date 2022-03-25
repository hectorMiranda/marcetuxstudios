---
layout: post
title: "Rust lifetimes, the first real encounter"
date: 2022-03-24
author: marcetux
tags: [rust, lifetimes, programming, learning]
---
The borrow checker is mostly invisible once ownership clicks — you move values when
you're done with them, borrow when you need to lend, and the compiler is satisfied.
Lifetimes are the part that stays invisible until it isn't, and this month I hit the
function where the compiler asked me to annotate them explicitly and I had to actually
understand what I was doing.

A lifetime annotation like `'a` in `fn foo<'a>(x: &'a str) -> &'a str` is the compiler
asking you to formalize a relationship: "the returned reference is valid for at least
as long as this input reference." The compiler can usually infer this — the "lifetime
elision" rules cover most common patterns — but once a function takes multiple reference
inputs and returns a reference, the compiler needs you to say which input the output
borrows from, because it can't guess from the types alone.

What helped me was stepping back from the syntax and thinking about what I'm actually
claiming. A function returning a borrowed value has to borrow it from somewhere — a
caller-supplied input or a `'static` value. The lifetime annotation is just making that
"from somewhere" explicit in a way the compiler can verify. Once I thought of it as
documentation of the borrow relationship rather than magic punctuation, writing the
annotations stopped being guessing and started being precision. The compiler is right;
you just have to say what you mean.
