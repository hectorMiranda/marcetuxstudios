---
layout: post
title: "Blazor is experimental but the idea is right"
date: 2018-04-14
author: marcetux
tags: [blazor, dotnet, webassembly, csharp, frontend]
---
Microsoft demoed Blazor at NDC Oslo last month and the GitHub repo went public. The
premise is running C# in the browser via WebAssembly — the Mono runtime compiled to
WASM, executing .NET assemblies client-side. The demo I watched was a Todo app, which
is the obligatory proof-of-concept, but you could see the shape of something real
underneath it.

The interesting part is not the "C# instead of JavaScript" surface feature, which will
be the flame war fuel. The interesting part is the component model and the fact that
shared code is actually shared — a validation library, a model class, an API client
interface — compiled once and running on both server and client without transpilation
or a separately maintained TS definition. That's the promise, and it's a legitimate
problem the JS ecosystem solves badly today with code generation and manual sync.

Calling it production-ready would be charitable to the point of dishonesty. The WASM
download is large, startup time is slow, and the tooling is rough in the way all
experimental tooling is rough. But this is clearly a Microsoft bet worth following. The
underlying platform, WebAssembly, is the real story — it makes the browser a legitimate
deployment target for any language, and Blazor is just the first serious .NET flag
planted there. Watching it evolve.
