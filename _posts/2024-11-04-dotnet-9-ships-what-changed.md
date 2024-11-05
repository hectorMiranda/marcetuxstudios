---
layout: post
title: ".NET 9 ships and what actually changed"
date: 2024-11-04
author: marcetux
tags: [dotnet, csharp, aspnet, runtime, performance]
---
.NET 9 shipped last week and I've been spending the weekend with it. The usual caveat:
most of the runtime improvements are invisible until you benchmark or are running at
scale. The things visible in day-to-day development are fewer, and I want to report
on those specifically rather than rehash the release notes.

The LINQ improvements I tracked in June landed as expected. `Count()`, `First()`,
`Last()` against `ICollection<T>` implementations now skip enumeration; `Order()` is
a cleaner alias for `OrderBy(x => x)` on types that implement `IComparable`. The
tensor primitives API in `System.Numerics.Tensors` is usable now — if you're doing
numerical work in .NET, this is what the ML.NET team has been building toward. Native
AOT got more capable: a larger fraction of ASP.NET patterns are AOT-compatible than
in .NET 8, which matters for cold-start latency in serverless deployments.

The thing I'll use most immediately: the improvements to `HybridCache` in ASP.NET
Core. It's a new multi-tier caching abstraction that sits over a local in-process
cache and a distributed cache (Redis), handles stampede prevention, and has a cleaner
API than manually coordinating `IMemoryCache` and `IDistributedCache`. Not
revolutionary, but it's the right abstraction for the pattern I've written five times
in the past year. LTS is .NET 10 next year; I'll run .NET 9 on consulting projects
where clients want current.
