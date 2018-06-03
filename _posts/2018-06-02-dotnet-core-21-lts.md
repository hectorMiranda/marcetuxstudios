---
layout: post
title: ".NET Core 2.1 is the LTS release worth switching to"
date: 2018-06-02
author: marcetux
tags: [dotnet, csharp, aspnet, backend, performance]
---
.NET Core 2.1 shipped this month and it's the one that earns the "long-term support"
label in a way that feels meaningful rather than aspirational. The performance
improvements are the headline — TechEmpower round 16 benchmarks show it competitive
with Go for plain JSON workloads — but for most applications the more relevant
improvement is Span<T> becoming a first-class API across the BCL. Less copying, fewer
allocations, and the kind of memory pressure improvements that don't show up in
microbenchmarks but absolutely show up in production GC pause metrics.

The things I reached for immediately: `IHttpClientFactory`, which solves the
`HttpClient` socket exhaustion problem that had tripped up at least two teams I know
of. Typed clients with a named factory, registered in DI, handling connection pooling
correctly — this is the pattern that was always the right answer but previously
required either manual pooling or a third-party library. Also `HttpClient` middleware
via `DelegatingHandler` chains for retry and auth, which is the interceptor model
from Angular applied to the HTTP client stack.

The guidance I'd give: if you're on .NET Core 2.0, upgrade. If you're on .NET
Framework and have been waiting for a reason, 2.1 LTS is a defensible target. The
porting friction I measured in January is lower now that a few more ecosystem packages
have shipped Core-compatible builds.
