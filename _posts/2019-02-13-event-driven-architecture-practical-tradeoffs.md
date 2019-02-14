---
layout: post
title: "Event-driven architecture and its practical tradeoffs"
date: 2019-02-13
author: marcetux
tags: [architecture, events, messaging, reliability, integration]
---
The bank's integration layer is moving toward event-driven patterns — services publish domain events to a bus (Azure Service Bus in our case) and consumers subscribe rather than calling each other directly. I'm a fan, with caveats, because I've watched the enthusiasm for EDA skip past the parts that are genuinely hard.

The thing that actually clicked: coupling comes in two flavors, and EDA trades temporal coupling (caller and callee must both be up at the same time) for a different kind of complexity — you now need to reason about eventual consistency, ordering, and replay. For payment status notifications that's a fine trade. The notification service doesn't need to block the payment service; it catches up from the bus. But for a balance inquiry that needs to be authoritative right now, you still make a synchronous call, and the EDA enthusiasts in the room need to accept that.

Operational discipline matters more than the architecture does. Schema evolution on events is where teams get burned: you publish an event type, three consumers bind to it, then you need to add a field. If you break the schema you break all three consumers at once with no coordination point. We're versioning event schemas explicitly — `payment.initiated.v1`, `payment.initiated.v2` — and running old and new versions in parallel until consumers migrate. More ceremony, but "the bus ate a malformed message and we don't know which consumers saw it" is a compliance incident, not a learning opportunity.
