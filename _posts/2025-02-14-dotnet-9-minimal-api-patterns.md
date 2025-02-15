---
layout: post
title: ".NET 9 minimal API patterns I'm actually using"
date: 2025-02-14
author: marcetux
tags: [dotnet, csharp, api, backend, platform]
---
A client greenfield brought me back to serious .NET work in February, and .NET 9 is
mature enough that the minimal API path is the obvious default rather than the trendy
alternative. Six months ago I would have reached for a controller-based setup out of
habit. Now the question is which minimal API patterns save ceremony without giving up
legibility.

The one I keep landing on is endpoint grouping with `MapGroup` — you attach
middleware, auth, and rate-limiting to the group once and all the endpoints inside
inherit it. This is the "do the work where it belongs" thing in a different coat: a
single route attribute on a controller was never the right level for cross-cutting
concerns, and a group makes the boundary explicit. Combine that with typed `IResult`
returns and the contract is visible without a doc generator.

The piece that still requires judgment is validation. Minimal APIs don't have model
binding validation out of the box the way controllers do, so you either wire in
FluentValidation as a pipeline behavior or you validate explicitly at the handler. I
prefer explicit — the handler reads as a policy document for what inputs are legal, not
a framework behavior you have to know to see. More characters, fewer surprises for the
next person.
