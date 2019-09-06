---
layout: post
title: ".NET Core 3.0 GA, day one impressions"
date: 2019-09-05
author: marcetux
tags: [dotnet, aspnetcore, release, architecture]
---
.NET Core 3.0 shipped this week and I had the reconciliation Worker Service migration staged and ready. The migration from the hand-rolled console app to the hosted Worker Service pattern went in an afternoon — most of the time was removing the custom main loop code and wiring the DI container setup to match what the generic host provides. The hosted `BackgroundService` handles the graceful shutdown on `SIGTERM` correctly, which was the hairiest part of the old code.

The version also brings the `System.Text.Json` serializer as a default replacement for Newtonsoft in ASP.NET Core. This is the upgrade with opinions: `System.Text.Json` is faster and allocation-friendly but strictly by-design in ways that `JsonConvert` users will find unexpected — case-sensitive property matching by default, no support for some of the more exotic Newtonsoft behaviors. We're keeping `Newtonsoft.Json` on the existing services and adopting `System.Text.Json` only on new services with clean contracts. Migration is not urgent; choosing correctly for new code is.

The C# 8 features that ship with 3.0 are what I've been waiting to use properly: nullable reference types, async streams, and switch expressions. Nullable reference types specifically — the compiler's ability to track `null` flow and warn on potential `NullReferenceException` paths — is the kind of static analysis that catches an entire class of production bug without a test. I've enabled it in nullable warning mode on the reconciliation service as the first trial. The warnings are illuminating; most are real.
