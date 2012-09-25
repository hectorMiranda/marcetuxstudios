---
layout: post
title: "SignalR and the push web"
date: 2012-09-24
author: marcetux
tags: [csharp, signalr, websockets, realtime]
---

I've been polling an endpoint every few seconds to keep a status page fresh, which
always feels like apologizing to the server. SignalR is the .NET answer to doing it
the right way: the server pushes when something changes.

What's clever is the **graceful degradation**. WebSockets are the goal, but they're
not everywhere in 2012 — old browsers, fussy proxies. SignalR negotiates the best
available transport: WebSockets if it can, then server-sent events, then long
polling, then forever-frame. Your code calls `Clients.All.somethingHappened(data)`
and SignalR figures out the plumbing for each connection.

The Hub abstraction is the nice part. The server defines methods clients can call
and can invoke methods *on* the clients — it reads like RPC in both directions. For
a dashboard that should reflect a job finishing the instant it finishes, that's
exactly the shape I want.

The caveat is scale-out: the moment you run more than one server, those in-memory
connections need a backplane (Redis or Service Bus) so a message published on one
node reaches clients connected to another. Worth knowing before you celebrate.
