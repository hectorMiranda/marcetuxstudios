---
layout: post
title: "Rust traits are the interface you're actually writing"
date: 2023-02-02
author: marcetux
tags: [rust, traits, architecture, casper]
---
After close to a year of Rust I finally have a confident answer for "what's the analog
to a C# interface?" — and it's traits, but they're both more and less than what I
expected coming from .NET. The comparison is useful for about five minutes and then
starts misleading you.

The more part: traits compose without inheritance. You don't declare that `Deploy`
extends some base class that holds the signing logic; you implement `Signable` for
`Deploy` and `Serializable` for `Deploy` separately, and then a function that needs
both just says `T: Signable + Serializable`. The constraint is on the call site, not
baked into a hierarchy you can't undo. That means you can add behavior to types you
didn't write, which is the `Display` and `From` trick I mentioned last month. The less
part: no dynamic dispatch by default. If you want "any type that implements this
trait," you're choosing between a generic (monomorphized at compile time, fast, larger
binary) and a `dyn Trait` (heap-allocated vtable, flexible, slower). Most contract code
wants the generic.

The design question that recurs is: at what boundary do I make this a trait? My current
heuristic is: if I'd mock it in a test, it should be a trait. The signature of a
good system is the one where the tests tell you where the seams belong.
