---
layout: post
title: ".NET 7 from the outside looking in"
date: 2022-11-04
author: marcetux
tags: [dotnet, csharp, release, performance]
---
.NET 7 shipped last week and I read through the release notes with the attention I
used to pay when it was my primary runtime. I'm writing Rust full-time now, so this is
more archaeology than practical preparation, but .NET remains relevant to me through
the tooling I maintain and the conversations I have with former colleagues who are
still in that world.

The performance numbers are the headline every year and they're real: the benchmarks
show another meaningful step forward in JIT output quality, particularly for
SIMD-heavy numerical code and for the cold-start path that had been a sore point in
serverless scenarios. The unified developer experience across workloads — the same SDK,
the same CLI, native AOT moving out of preview — is the long-arc story of the past
few releases playing out. It's a healthier ecosystem than the .NET Framework era, and
I say that as someone who wrote a lot of `web.config` in anger.

The feature I'd have been most excited about as a working C# developer is the
improvements to `ref struct` and the `required` members feature for safer initialization.
Those are type-system improvements in the direction Rust has been pointing — making the
compiler verify invariants that used to be enforced only by convention. The languages
are converging on the insight that the type system is the best place for correctness
constraints. C# is getting there from a different starting point, but it's getting there.
