---
layout: post
title: "Blazor WebAssembly after six months in production"
date: 2020-11-13
author: marcetux
tags: [dotnet, blazor, webassembly, csharp]
---
The internal reporting tool I launched in Blazor WebAssembly in May has been in
production for six months. Small user base — operations staff, around twenty
concurrent users at peak — but real use, real support tickets, real lessons.

The first-load concern I documented in May is real but not the problem I expected.
On the corporate network, the initial download is fast enough that users don't
complain about it, and subsequent loads are instant from the cache. Where the
download did matter was the laptop that was on a hotel WiFi at 3 Mbps — Blazor
WASM is not the right choice for users on intermittent connections unless you invest
in lazy loading and aggressive PWA caching. We added a service worker and lazy-loaded
the heaviest assemblies; first paint is now fast even on slower connections.

The browser debugging story is better than I expected. Source maps from the IL to
browser position work in Chrome DevTools, though stepping through compiled C# in a
browser is still not quite the Visual Studio experience. The production question I'd
ask before choosing Blazor WASM again: who are your users, what are their connections,
and how often do they return? Frequent users on fast networks — internal tools
— are the sweet spot. Public sites with new visitors who may not return to warm the
cache are a harder conversation.
