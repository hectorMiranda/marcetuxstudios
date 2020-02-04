---
layout: post
title: "Blazor Server in production after three months"
date: 2020-02-03
author: marcetux
tags: [dotnet, blazor, csharp, frontend]
---
We put our first Blazor Server app in front of real users in November and now have
enough data to have an opinion. The app is an internal loan-operations dashboard —
not a customer-facing product — which is exactly the right first context for a
technology that's still finding its footing.

The model is unusual enough that colleagues coming from React or Angular need a
minute to internalize it: the component tree runs on the server, and a SignalR
websocket carries the diff back to the browser. Rendering is .NET, not JavaScript.
That sounds like a constraint until you realize it means the same C# validation
logic, the same domain model, zero serialization boundary between UI and data. The
loan-calculation code that lives in a shared library just works inside a component
without any API layer in between.

The production concern everyone asks about is the connection. If the websocket drops,
the UI goes dark until reconnection. For an internal tool on a corporate network that's
a minor annoyance; for a public site with mobile users it's a harder conversation.
Circuit size — the server memory per connected user — matters at scale in a way it
doesn't for three concurrent ops staff. For this use case the tradeoffs landed in the
right column. For the next one I'd look at Blazor WebAssembly, if it ships GA this
year the way the roadmap says it should.
