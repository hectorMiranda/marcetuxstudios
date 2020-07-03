---
layout: post
title: ".NET 5 preview and unifying the platform"
date: 2020-07-02
author: marcetux
tags: [dotnet, csharp, architecture, platform]
---
Preview 6 of .NET 5 dropped this week and I've been spending evenings with it.
The headline for the .NET 5 cycle is unification — one SDK, one BCL, one toolchain
for web, desktop, mobile, cloud, and microservices instead of the parallel .NET Core
/ .NET Framework / Xamarin tracks. The `net5.0` TFM replaces the alphabet soup of
target framework monikers. For teams like ours that have been on .NET Core for two
years, the immediate impact is smaller; for teams still hedging on Framework, this
is the unmistakable signal to move.

The C# 9 additions that ship with it are the ones I keep coming back to. Records
are the feature I've wanted for data-transfer objects since I stopped writing F#:
a `record` type gives you structural equality, a non-destructive `with` expression
for copying with modifications, and a concise positional syntax. A DTO that was eight
lines of boilerplate property declarations plus an Equals override is now one line.
Pattern matching improvements in switch expressions clean up the kind of
discriminated-union-like dispatch that I've been approximating with abstract classes.

The November ship date is the one I'm tracking. Preview quality is good — I've
converted a small internal console tool and the migration was friction-free. The
bigger projects will wait for RC, which is the right call. But I'm already writing
C# 9 in the things I can, because the records and the init-only setters are
just better and there's no reason to wait.
