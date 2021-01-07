---
layout: post
title: "What .NET 5 actually changes at work"
date: 2021-01-06
author: marcetux
tags: [dotnet, csharp, architecture, azure]
---
We shipped .NET 5 in November's announcement window and I finally have a month of
real use behind it. The "unified platform" pitch lands more honestly than it sounds
in keynotes: we stopped maintaining separate .NET Framework and .NET Core project
files for shared libraries, and that alone saved an afternoon per sprint of
mysterious compatibility whack-a-mole.

The C# 9 record types showed up in our DTO layer almost immediately. A request
object that's just data — ten properties, no behavior — is now a one-liner record
instead of a class with a constructor, equality override, and a ToString I kept
forgetting to maintain. The pattern matching improvements in switch expressions
also made the mapping layer more readable; the code says what it's doing instead
of stacking ternaries. Small wins, but they compound across a large codebase.

The thing I keep reminding the team: .NET 5 is not a rewrite opportunity. Every
greenfield-leaning PR that shows up citing the upgrade is the same feature that
didn't ship last quarter in a new coat. Migration budget goes on paying down a
specific list of technical debt, not redecorating. The platform got better; the
discipline of delivery has to get better too.
