---
layout: post
title: ".NET 9 minimal API improvements worth knowing"
date: 2026-03-05
author: marcetux
tags: [dotnet, minimal-api, csharp, aspnet]
---
We're running .NET 8 in production at AmaWaterways, and I've been poking at the .NET 9 features in a local branch to understand what the upgrade path looks like when we get there. The minimal API story — which is the pattern we're using for the identity services — got meaningful improvements in 9 that address some of the real friction in 8.

The one I'm most interested in is the request delegate generator improvements that bring minimal API endpoint handler parameter binding closer to parity with controllers without the ceremony. In 8, complex binding scenarios sometimes still required an explicit `[AsParameters]` attribute and a record type to aggregate query parameters. In 9, the inference covers more cases cleanly. More importantly for our use case, the OpenAPI generation has matured — we had to use a third-party package to get accurate schema output in 8, and the built-in support in 9 is close enough that we can probably drop the dependency.

What I'm not rushing: .NET 9 is a current-release, not an LTS. .NET 10 will be the next LTS in late 2026. For a production identity service, I'd rather ride 8 LTS → 10 LTS than do two upgrades in a year. But knowing what's in 9 tells me what the 10 LTS baseline will look like, which is useful planning context. The upgrade pressure in identity services is different — you're not upgrading because you want new features; you're upgrading because staying current with the security patch cadence is the job.
