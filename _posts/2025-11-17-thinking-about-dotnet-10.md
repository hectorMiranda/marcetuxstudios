---
layout: post
title: "Thinking about .NET 10 from the preview builds"
date: 2025-11-17
author: marcetux
tags: [dotnet, csharp, backend, platform, preview]
---
The .NET 10 preview builds are in the interesting-but-not-production phase and I've
been running the preview SDK in a side project for a month. The release cadence is
reliable enough that previewing at this stage gives a useful signal about what to plan
for next year without committing to anything that might change. The improvements I'm
watching.

The native AOT story is better in each release and .NET 10 continues that trend. Smaller
binaries, broader API coverage, better diagnostics when a type doesn't work with AOT. For
microservices that need fast startup and low memory, the AOT path is getting viable for
more of the stack. The minimal API pattern makes this particularly accessible — a handler
that doesn't reach for reflection-heavy libraries is a candidate for AOT with minimal
annotation work.

The LINQ and collection improvements are the kind of incremental that adds up. Better
`Span<T>` integration, additional LINQ operators that avoid allocation, collections that
compose without copying. None of these are dramatic; all of them matter in hot paths.
The .NET performance team blogs the benchmarks and they're consistently moving in the
right direction. The framework is getting faster without asking the developer to rewrite.
