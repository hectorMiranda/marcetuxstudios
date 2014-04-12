---
layout: post
title: "Redis pub/sub for real-time portal notifications"
date: 2014-04-11
author: marcetux
tags: [redis, pubsub, realtime, dotnet, websockets]
---
The SignalR hub from last year pushes bandwidth data to connected dashboards, and the
question came up: what publishes to it? On a single server it's a direct method call.
With two web servers, a push generated on server A needs to reach browsers connected
to server B. Redis pub/sub is the backplane that makes that work, and it's about
as simple as a backplane gets.

Redis pub/sub is a message fan-out mechanism: a publisher sends a message to a named
channel and every subscriber to that channel receives a copy. The message is fire-and-forget
— Redis doesn't persist it, and subscribers who aren't connected when the message
publishes miss it. For live dashboard updates that's fine; a brief disconnection means
a few missed data points, and the next real data point catches up the view. The SignalR
Redis backplane package wires this automatically: instead of calling hub methods directly,
each server publishes to a Redis channel, and every server that runs a hub subscriber
forwards the message to its connected browsers.

The configuration is two lines in the SignalR startup: `GlobalHost.DependencyResolver
.UseRedis("localhost", 6379, password, appName)`. After that, which physical server a
browser connects to stops mattering. The session is sticky by load-balancer cookie for
the SignalR connection itself; the data fan-out is Redis. A clean separation between
transport and distribution.
