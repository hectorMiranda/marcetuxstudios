---
layout: post
title: "Blazor Server GA, an honest assessment"
date: 2019-09-09
author: marcetux
tags: [blazor, dotnet, frontend, aspnetcore, architecture]
---
Blazor Server shipped as generally available in .NET Core 3.0 and I spent a weekend building a prototype of an internal reporting dashboard with it to form an actual opinion. The idea: write C# components, run the rendering on the server, and keep a SignalR connection open to push DOM diffs to the browser. No JavaScript required. Blazor WebAssembly is still experimental; this is the server-side-rendering model.

The prototype worked surprisingly well for a read-heavy dashboard with real-time data. The Razor component model is coherent — components are C# classes with a markup template, state is just fields, and re-renders happen when state changes. Coming from Angular, the model is simpler to reason about for someone who thinks in C#. Authentication integrates directly with ASP.NET Core auth middleware, so the banking portal's claims-based access control comes for free. That's not a small thing.

The honest limitations for our production case: every connected user holds a SignalR connection and server-side state. With hundreds of concurrent portal users, the memory and connection overhead per server is non-trivial. Contrast with an Angular SPA where the server is stateless after serving the initial bundle. The latency of the connection also means Blazor Server is not great for highly interactive UIs where every keystroke triggers a render. For a reporting dashboard with a few controls and mostly data display, it's fine. For a payment form where responsiveness matters under variable network conditions — the Angular SPA stays. Right tool, known constraints.
