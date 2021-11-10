---
layout: post
title: "C# 10 record structs and when they replace classes"
date: 2021-11-09
author: marcetux
tags: [csharp, dotnet, design, performance]
---
C# 10 ships with .NET 6 and the new language feature I've been anticipating since
I started using record classes in C# 9 is `record struct`. A record class gave me
structural equality and immutability for value objects; it was still a heap-
allocated reference type. `record struct` gives me the same generated members —
`Equals`, `GetHashCode`, `ToString`, the `with` expression — in a value type that
lives on the stack.

The domain objects that are genuinely value-like but short-lived qualify: a
coordinate pair used in a geometry calculation, a time-range tuple used in a
query filter, a money-amount-and-currency pair used in a pricing loop. These are
small, fixed-size, created in large numbers, and never shared across threads. Heap
allocation for each of them means GC pressure; stack allocation means they're
freed when the method returns. For a pricing batch that creates millions of these
objects, the GC pause difference is measurable.

The discipline: `record struct` is not a drop-in replacement for `record class`
everywhere. A value type is copied on assignment — no aliasing, no null reference —
which is the right model for a value object but the wrong model for an entity with
identity. Use `readonly record struct` to signal that the struct itself is
immutable, which also tells the compiler it can pass the struct by reference
internally as an optimization. The language keeps getting better at expressing what
you mean precisely; the skill is knowing which precision matters for your problem.
