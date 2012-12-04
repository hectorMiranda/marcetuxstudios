---
layout: post
title: "Scaling SignalR past one box"
date: 2012-12-03
author: marcetux
tags: [signalr, redis, realtime, scaling]
---
I flagged this in the SignalR post and now it's bitten me: the moment you run two
web servers, real-time stops working for half your users. A client connected to
server A never hears a message published on server B, because those in-memory
connections don't know about each other.

The fix is a **backplane** — a shared bus every server publishes to and subscribes
from. SignalR ships a Redis backplane: each server forwards its outgoing messages
through a Redis pub/sub channel, and every server relays what it sees to its own
connected clients. Add the backplane, change no application code, and broadcasts
reach everyone again.

The cost is honesty about what you've built: a message now does a network hop
through Redis before fan-out, so it's slightly slower and Redis is now on the
critical path for real-time. For a status dashboard that's a fine trade. For a
trading feed you'd think harder. Know which one you're building.
