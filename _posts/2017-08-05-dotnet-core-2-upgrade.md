---
layout: post
title: ".NET Core 2.0 upgrade and what the bigger API surface actually means"
date: 2017-08-05
author: marcetux
tags: [dotnet, aspnetcore, architecture, upgrade]
---
.NET Core 2.0 landed last week and the headline — 20,000 APIs compared to Core 1.x's
13,000 — is correct but understates the practical effect. The APIs that were missing
from Core 1.x weren't random; they were the ones that most existing .NET Framework code
actually uses. `DataTable`, binary serialization, `HttpWebRequest`, a large chunk of
`System.Drawing`. Their arrival in 2.0 isn't about new features; it's about the
porting tax going from "audit everything and rewrite the incompatible parts" to "update
the target framework and fix a handful of warnings."

I ran the API Compatibility Analyzer against the oldest internal service — the pricing
engine, a .NET Framework 4.6 project we'd been deferring — and came out the other side
with eight incompatibilities, down from twenty-three when I'd done the same check
against Core 1.1 in February. Three of the eight were binary formatter usage, which I'd
want to replace with a real serialization library regardless of the migration. The other
five were addressable in a day.

The deployment story is also cleaner in 2.0. The self-contained deployment mode bundles
the runtime with the app — the target machine doesn't need .NET Core installed. For
containers this is less exciting, but for the occasional Lambda or bare-metal deployment
it's the difference between "works" and "works after a manual runtime install step that
someone forgot." More boring deployments, fewer surprises.
