---
layout: post
title: ".NET 8, the highlights that matter"
date: 2023-11-02
author: marcetux
tags: [dotnet, csharp, performance, aspnet]
---
.NET 8 released this week. I haven't shipped C# professionally since early 2022, but
I've been watching the release notes because the platform keeps improving in ways that
matter even when it's not your day job. And looking at the interview landscape, there
are more AI-layer roles sitting on top of .NET backends than you'd expect — the
enterprise world doesn't rewrite its API layer because the AI space gets exciting.

The performance story continues to be the headline. The JIT's Profile-Guided
Optimization now applies more broadly, and the startup time improvements make
.NET 8 genuinely competitive with Go for latency-sensitive services. The new Frozen
Collections — `FrozenDictionary`, `FrozenSet` — are immutable lookup structures
optimized for read-heavy workloads where the keys are known at startup. If you have
a configuration dictionary that never changes, the frozen version is measurably faster
on repeated reads.

The one I'll actually use: the `TimeProvider` abstraction is now built in. Testing time-
dependent code in .NET used to require either injecting a `Func<DateTime>` or reaching
for a third-party library. Now there's a real abstraction — `TimeProvider.System` for
production, a fake implementation for tests. That's the kind of boring, correct fix that
makes me trust a platform. Someone made the right decision instead of the easy one.
