---
layout: post
title: "Scaling SignalR across web servers with a Redis backplane"
date: 2013-05-02
author: marcetux
tags: [dotnet, signalr, redis, realtime, scalability]
---
Adding a second web server behind the load balancer exposed the gap in the SignalR
setup immediately. Browser A connects to web1 and joins the bandwidth feed for
customer 12. Server-side code pushes a sample, but the message goes to web1's in-process
Hub. Browser B, connected to web2, sees nothing. Two servers, two islands of clients,
no coordination — the whole push model breaks down.

The fix is a backplane: a shared message bus that every server publishes to and reads
from. SignalR ships an official Redis backplane package. The setup is one `GlobalHost
.DependencyResolver.UseRedis(...)` call in `Startup.cs` with the Redis connection
string. After that, a Hub message published on web1 goes to Redis; every SignalR server
subscribed to that Redis channel delivers it to its own connected clients. The clients
on web2 get the message. From the browser's perspective, nothing changed.

The latency penalty is sub-millisecond on our Redis box, which is acceptable for
bandwidth samples that are themselves sampled at one-second intervals. The bigger
discipline is thinking about messaging earlier — the backplane assumption should have
been in the first Hub design, not retrofitted when we added a server. Stateless web
servers with a shared bus is a pattern, not an afterthought.
