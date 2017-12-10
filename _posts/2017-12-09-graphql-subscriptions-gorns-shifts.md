---
layout: post
title: "GraphQL subscriptions for real-time shift updates"
date: 2017-12-09
author: marcetux
tags: [graphql, websockets, nodejs, architecture]
---
The shift feed — the list of available shifts that nurses see when they open the app —
needs to update in near-real-time when a facility posts a new shift. The push
notification handles the alert; the app also needs to refresh the list without the
nurse manually pulling down to refresh. The previous implementation used a 60-second
polling interval, which is visible lag that healthcare workers notice and complain about.

GraphQL subscriptions are the right model here. A subscription is a persistent WebSocket
connection where the server pushes data when something the client is subscribed to
changes. The client subscribes to `newShiftPosted(location: $coords, radius: 25)` and
the server pushes a new shift object whenever one is posted within that radius. The
client gets the data already shaped the way it needs it — same query shape as the
initial load — rather than a generic "something changed, go fetch it" notification.

The implementation uses `graphql-subscriptions` and `subscriptions-transport-ws` on the
Node backend. The server tracks active subscriptions in memory — fine at our current
scale — and publishes to the `ShiftPosted` topic whenever the shifts table gets a new
row. The PubSub mechanism is in-memory now and will need to move to Redis when we have
more than one Node process, but that's a change I can make without touching the
subscription schema. The protocol doesn't care about the PubSub transport. That boundary
is the part I like most about the design.
