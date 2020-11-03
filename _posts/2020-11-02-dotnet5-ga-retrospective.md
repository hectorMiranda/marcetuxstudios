---
layout: post
title: ".NET 5 ships - a year of previews pays off"
date: 2020-11-02
author: marcetux
tags: [dotnet, csharp, platform, retrospective]
---
.NET 5 is out. I've been running previews since April and production migrations since
RC1, so the GA is less a revelation than a permission slip — the features are known;
now they're supported. The thing worth writing about is what the unified platform
actually means after a year of watching the roadmap become code.

The platform story is real. One `net5.0` target, one SDK, the same BCL everywhere.
The project files got simpler; the implicit usings and nullable reference types are
defaults you can enable without scaffolding. For our team the immediate win is that
`netstandard2.0` shared libraries can target `net5.0` directly now that Framework
clients are off the table. No more TFM gymnastics to produce a package that works
in both worlds.

The runtime performance improvements are not marketing. I ran the ASP.NET Core
benchmarks from the dotnet/performance repo against our 3.1 baseline and our 5.0
branch. Throughput on the gateway service is up about 15% with no code changes — just
the target framework bump. `System.Text.Json` contributes some of that; the GC
improvements contribute the rest. Six months of preview work, two migration passes,
and a net result of faster software for free. The effort-to-payoff ratio on a .NET
major version is better than almost anything else in the stack.
