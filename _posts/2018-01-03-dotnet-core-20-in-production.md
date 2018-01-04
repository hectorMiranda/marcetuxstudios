---
layout: post
title: ".NET Core 2.0 in production"
date: 2018-01-03
author: marcetux
tags: [dotnet, csharp, aspnet, architecture, backend]
---
We flipped the Go RN API to .NET Core 2.0 in December and the new year is the first
month where I can actually call it production-proven. The jump from 1.x to 2.0 is
larger than the version bump implies — the BCL surface area nearly doubled, and the
things that were missing before, `ConfigurationBuilder`, decent EF Core support, the
full `System.Net.Http` stack — are there now. It finally feels like a first-class
runtime rather than a science project.

The performance story is where I feel the decision paying off. Our patient scheduling
endpoints handle a spike pattern — clinics opening in the morning — and the Kestrel
throughput on .NET Core is noticeably better than the old ASP.NET 4.6 stack it
replaced. We're on the same EC2 instances, fewer 502s under load, and startup time
dropped enough that cold Lambda invocations stopped timing out. Those are the numbers
a small startup team can actually act on.

The migration cost was real: about three weeks of porting, mostly around things that
didn't make the cut into Core — `System.Drawing`, a couple of reflection tricks,
and a dependency that only shipped a full-framework build. The lesson is to budget for
that dependency audit before starting, not after. But two months out, the answer is
yes — Core 2.0 is the bet worth making.
