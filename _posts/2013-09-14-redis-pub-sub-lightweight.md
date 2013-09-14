---
layout: post
title: "Redis pub/sub as a lightweight message bus"
date: 2013-09-14
author: marcetux
tags: [redis, messaging, dotnet, architecture, scalability]
---
The SignalR Redis backplane uses Redis pub/sub under the hood, and understanding that
backplane made me look at pub/sub directly for a different problem: notifying multiple
internal services when a customer's configuration changes. The previous mechanism was
polling the database, which is the correct answer to nothing.

Redis pub/sub is simple by design: a publisher calls `PUBLISH channel message` and
every client subscribed to that channel receives the message. No persistence — a
subscriber that isn't connected when the message publishes misses it. No acknowledgment.
No queuing. It's fire-and-forget at the delivery layer, which makes it wrong for
anything where message loss is unacceptable.

For the configuration-change notification, message loss is acceptable: the subscriber
can re-read the current configuration from the database on reconnect. The notification
is "something changed" rather than "here is the value that changed," so a missed message
means a subscriber is slightly stale until it reconnects or until the next change fires.
For that specific use case, pub/sub is exactly the right level of complexity. The
temptation with any message-bus capability is to reach for it everywhere; the discipline
is noticing that durable, acknowledged message queuing (which we'd need for something
like order processing) is a different tool with a different cost model. Use the simple
thing where simplicity's failure modes are tolerable.
