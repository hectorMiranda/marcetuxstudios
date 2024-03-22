---
layout: post
title: "C# primary constructors and what they change day to day"
date: 2024-03-21
author: marcetux
tags: [csharp, dotnet, language, architecture]
---
I've been doing some .NET work for a consulting client and caught up on C# 12, which
landed with .NET 8 last November. Primary constructors for non-record classes are
the feature I keep reaching for, and I want to write down why instead of just
quietly preferring them.

The before: a class with three injected dependencies needs a field for each, a
constructor parameter for each, and an assignment in the constructor body for each.
That's nine lines of code — field declaration, parameter, `this.X = x` — for zero
logic. A developer reading it has to mentally merge three parallel lists to
understand what the class holds. Primary constructors collapse that: the dependency
appears once, in the class signature, and is available by name throughout the class.
The reader sees what the class needs in one place and the class body is what the
class *does*.

The caveat worth knowing: primary constructor parameters are *captured*, not stored.
If you store one in a field yourself AND capture it, you have two copies. The
convention I've settled on: use the parameter directly everywhere and only declare a
field when you genuinely need the stored copy to outlive construction in a way the
capture won't give you. It's a small discipline and the readability payoff is large.
Not every feature change how you write code; this one does.
