---
layout: post
title: "SignalR 1.0 and a live dashboard"
date: 2013-02-11
author: marcetux
tags: [dotnet, signalr, realtime, websockets, frontend]
---
SignalR hit 1.0 this month, which is the excuse I needed to kill the worst pattern in
the dashboard: polling. The bandwidth view asks the server "anything new?" every few
seconds, which is wasteful when nothing changed and laggy when something did. Real
push is the right answer, and SignalR finally makes it boring to set up.

The model is a **Hub** — a server class whose methods the browser can call, and which
can call back into every connected browser. Under the hood it negotiates the best
transport available: real WebSockets where the browser and proxies allow it, and a
graceful fall back to long-polling or server-sent events where they don't. That
fallback is the whole value — I write push-style code once and don't care that half
our corporate clients are behind a proxy that mangles WebSockets.

So the dashboard flips: instead of the client asking, the server pushes a row the
instant a new bandwidth sample lands, and the Angular view updates. Less traffic,
instant updates, and the polling timer is gone. The one wrinkle is scale-out across
web servers — same backplane problem I hit with the old SignalR — but for a handful
of dashboards on one box it Just Works. Hub's in `examples/`.
