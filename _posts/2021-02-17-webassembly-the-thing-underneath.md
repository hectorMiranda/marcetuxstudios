---
layout: post
title: "WebAssembly as an execution target not a language"
date: 2021-02-17
author: marcetux
tags: [wasm, architecture, frontend, dotnet]
---
Blazor WebAssembly shipped as stable last May and I've been running a small
internal tool on it since November, which gives me enough operational time to have
a real opinion. The tool is a rules-configuration UI: complex enough to validate
the tech, simple enough that I'm not apologizing for the choice if it fails. One
thing that crystallized through using it: WebAssembly is a compile target, not a
language, and mixing that up leads to wrong expectations.

The common framing is "run C# in the browser," which is true but incomplete. What's
actually happening is the .NET runtime — itself compiled to WASM — running inside
the browser's sandbox. That means the startup payload is the runtime plus your
application code, which is why the first load is a 3–5 MB download even for a small
app. Once cached, subsequent loads are fast; the first load is a conversation you
have to be ready for.

Where it earns its keep is code sharing. The validation logic that lives in the
domain layer on the server runs verbatim in the browser without a TypeScript
rewrite, and without keeping two implementations in sync. For the rules-config tool
that's worth the startup cost: the same validator runs client-side for instant
feedback and server-side for the authoritative check, and there's one test suite
for both. The reuse case has to be real to justify the weight. If you're mostly
rendering dynamic HTML, something lighter will serve you better.
