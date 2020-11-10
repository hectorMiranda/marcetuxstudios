---
layout: post
title: "Apple Silicon M1 and what it means for a .NET developer"
date: 2020-11-09
author: marcetux
tags: [hardware, dotnet, platform, tooling]
---
The M1 MacBook Air and Pro shipped this week and the benchmark numbers being posted
are difficult to believe, then you read enough of them from enough independent sources
to accept they're real. Single-core performance beating Intel laptops at three times
the price, while running on a fraction of the power. ARM64 in a laptop has arrived
in a way the Surface Pro X never quite managed.

The .NET story on M1 is in transition. .NET 5 runs under Rosetta 2 emulation —
the translation layer Apple provides for x86-64 binaries — and Rosetta is remarkably
good: emulated .NET 5 on M1 benchmarks faster than native .NET 5 on many Intel
machines. The native ARM64 .NET runtime is in preview targeting .NET 6, which means
the emulation path is the production path for now. Given Rosetta's speed, that's not
a hardship.

For the home lab, this is interesting at a different level. The k3s cluster runs on
ARM64 Pi 4s; the container images I build target ARM64. A Mac that's natively ARM64
means the local development and the cluster architecture are finally the same ISA.
Cross-compilation complexity goes away when the dev machine and the production node
are the same architecture. I'm not buying an M1 this month — my 2019 Intel MBP is
fine — but the next machine cycle will be an easy decision.
