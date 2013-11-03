---
layout: post
title: "SignalR reconnect behavior during rolling deploys"
date: 2013-11-02
author: marcetux
tags: [dotnet, signalr, websockets, realtime, devops]
---
The rolling deploy process restarts one web server at a time, which is fine for
stateless HTTP requests that the load balancer routes to the live server. SignalR
connections are stateful — a browser holding a WebSocket to web1 loses it when web1
restarts. I tested what actually happens to connected clients during the restart window,
and the behavior is better than I expected but requires configuration.

SignalR's client-side JavaScript library has built-in reconnection logic. When the
connection drops, the client enters a `reconnecting` state and attempts to re-establish
the connection at increasing intervals, up to a configurable timeout. If the server comes
back up (or the load balancer routes to the other server) within the timeout window,
the connection re-establishes and Hub method subscriptions are re-registered by the
client's `connection.start()` callback. The browser dashboard briefly shows a
"reconnecting" indicator and then resumes, without losing the page.

The configuration that matters: the load balancer's session stickiness was already
disabled (from the Redis session work in March), so a reconnecting client can land on
either server. The Redis backplane means the new connection gets the same channel
subscriptions regardless of which server handles it. The one gap: if a client was in
the middle of a Hub method when the server restarted, that call is lost and not retried.
For the dashboard it's acceptable — the next push sample fills the gap. For anything
that requires exactly-once delivery, SignalR isn't the right bus.
