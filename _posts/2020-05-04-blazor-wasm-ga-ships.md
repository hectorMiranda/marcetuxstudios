---
layout: post
title: "Blazor WebAssembly goes GA and what that means for .NET SPAs"
date: 2020-05-04
author: marcetux
tags: [dotnet, blazor, webassembly, csharp]
---
Blazor WebAssembly shipped GA with .NET Core 3.1.3 this week, and after two months
on the preview I can say the production build is meaningfully better — startup time
is down, the AOT compilation story is clearer, and the lazy loading support lets you
cut the initial download by deferring assemblies you don't need on the first route.
This is no longer a preview you're betting on; it's a supported Microsoft release.

The use case I'm pitching internally is the same one that made Blazor Server worth
doing: teams that already own .NET and don't want to maintain two languages at the
boundary. A Blazor WASM app calls .NET APIs, shares model classes from a common
project, uses the same validation attributes in both places. The JavaScript you write
is the JavaScript you can't avoid — a small interop file for browser APIs that .NET
doesn't expose. Everything else is C#.

The honest caveat is still the initial payload. The first load downloads the .NET
runtime plus the app assemblies — compressed, but still meaningful. For customer-
facing public sites where first-load performance is a product decision, I'd have the
conversation. For internal tools where users are on a corporate network and will
return daily, the cache primes after the first visit and then it's fast. Know your
audience before you know your framework.
