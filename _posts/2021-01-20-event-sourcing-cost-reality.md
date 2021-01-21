---
layout: post
title: "Event sourcing and what it actually costs"
date: 2021-01-20
author: marcetux
tags: [architecture, event-sourcing, databases, design]
---
We evaluated event sourcing for a new customer-ledger service and the meeting where
it was proposed had the usual energy: immutable history, temporal queries, audit log
for free. All true. The decision we made — to not use it — took longer to explain
because you can't point at a feature it lacks, only at the operational cost it adds.

The hidden weight is in the read side. Event sourcing buys you a perfect write log
and charges you a read model you have to build and keep synchronized. For a simple
query like "what is the current balance?" you need a projection — something that
replays or reacts to the event stream to produce the answer. That projection is
a service. It needs deployment, monitoring, a rebuild story when you change the
event schema, and someone who understands it at 2 a.m. For audit history we can
get another way, that cost is real money and real pages.

The architecture that won is a conventional write model with an explicit audit table
— a shadow insert that captures before/after for every mutation. Boring, but the
junior engineer on call can debug it, the DBA can query it directly, and it doesn't
introduce a new failure mode for the main read path. Event sourcing is a real tool
for problems where the history *is* the primary data. When history is secondary
and the current state is what you serve, think twice before buying the projection
engine to get it.
