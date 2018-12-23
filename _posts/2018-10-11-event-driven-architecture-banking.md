---
layout: post
title: "Event-driven architecture at a bank and what it actually requires"
date: 2018-10-11
author: marcetux
tags: [enterprise, messaging, architecture, azure, integration]
---
The API modernization program is landing on Azure Service Bus as the backbone for
asynchronous flows, and the architecture discussion this month was about what
"event-driven" actually requires at a regulated institution versus what it means in
a startup where you control both ends of every message. The constraints that
change the answer: audit trail requirements, replay policies, and the fact that your
consumers are not all systems you control or can update quickly.

The audit requirement alone shapes the design more than the technology choice. Every
event that drives a state change must be persisted before the action it causes, and
that persistence must survive any single component failure. In practice: write to
the audit log and the Service Bus topic in the same transaction, or accept that an
event can be acknowledged but not acted upon. We're using the outbox pattern — the
event goes into a database table in the same transaction as the business record, and
a publisher process reads the outbox and publishes to Service Bus. Slower, simpler,
and auditable.

The consumer side requires idempotency. Service Bus delivers at-least-once; any
consumer that isn't idempotent will create duplicate state changes when redelivery
happens, and it will happen — network partition, consumer restart, lease expiry.
Idempotency key per message, checked against a processed-message log before taking
action. The discipline is making idempotency a build requirement for every consumer,
not an afterthought when a bug report arrives.

*Update, from the deployment freeze reading: the outbox poller interval deserves more
care than I gave it in October. Too frequent and you add unnecessary database load
during high-throughput periods; too infrequent and your event latency grows to minutes
during low-volume windows. We landed on an adaptive interval — start at 100ms,
back off to 2s exponentially if the outbox is empty for several consecutive polls,
reset immediately on insert via a database notification. That keeps event latency low
when there's work and quiet when there isn't.*
