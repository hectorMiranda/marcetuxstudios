---
layout: post
title: ".NET 9 preview and what I am watching"
date: 2024-06-26
author: marcetux
tags: [dotnet, csharp, aspnet, performance, runtime]
---
.NET 9 previews have been landing and I've been skimming the release notes between
consulting work. The GC and JIT improvements are always there — each release shaves
another few percent off throughput benchmarks — but the things I'm actually watching
for are more architectural than raw performance.

The LINQ performance work is interesting. `First`, `Last`, `Count` on types that
implement `IList<T>` or `ICollection<T>` now skip the enumeration and go direct. Not
a big deal for small sequences; meaningful when you're chaining over large data sets
and had been considering ToList() as a workaround. The tensor primitives work that
started in 8 is continuing — `System.Numerics.Tensors` is getting a more complete
API, which is clearly aimed at the ML-in-.NET story alongside ML.NET and the ONNX
Runtime integration.

What I'm most interested in: the improvements to the minimal API and native AOT story.
Compile to a native binary, small startup, low memory — that's the shape of a
serverless or container workload in 2024, and .NET has been closing the gap with Go
and Rust for that use case. I'm still on .NET 8 for the consulting work because
clients want LTS, but I'm staying current on 9 so I can give honest recommendations
on timing. The gap in November is when it ships; that's when I'll write the real
accounting.
