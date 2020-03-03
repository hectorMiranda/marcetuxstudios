---
layout: post
title: "Blazor WebAssembly preview and what runs in the browser now"
date: 2020-03-02
author: marcetux
tags: [dotnet, blazor, webassembly, csharp]
---
The Blazor Server app from November finally prompted me to look at the other half of
the Blazor story. WebAssembly mode flips the execution model: instead of the component
tree running on the server with a SignalR wire to the browser, the .NET runtime itself
compiles to WebAssembly and ships to the client. The app runs locally in the tab,
calling your APIs like any other SPA.

The preview build I pulled this week is genuinely interesting. A Razor component is
the same code whether it targets Server or WebAssembly; the rendering model is the
abstraction that changes underneath. What I wanted to verify was call latency — every
API interaction is a real network request now, no in-process shortcut — and for the
forms-heavy internal tools we build, that's fine. The initial download is heavy
(the .NET runtime plus the app), but it caches aggressively and the interaction after
load is local.

The GA date is supposedly May. I'll wait for that before putting it in front of
anyone at work, but the preview behavior is solid enough that I'm comfortable
committing to it. The promise — C# in the browser, shared model between server and
client, no JavaScript bundler — is real. It's not the right tool for every SPA, but
for teams that live in .NET it removes a context switch that adds up.
