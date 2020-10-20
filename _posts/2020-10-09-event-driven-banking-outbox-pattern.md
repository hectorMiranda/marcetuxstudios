---
layout: post
title: "The outbox pattern and reliable event publishing in banking"
date: 2020-10-09
author: marcetux
tags: [architecture, messaging, dotnet, banking]
---
Publishing a domain event and updating the database in the same operation has a
race: if the event publishes successfully but the database commit fails (or vice
versa), you have inconsistent state. In retail software that's a retry away from
fine. In banking, where the event triggers downstream loan processing, an event
without a corresponding database record — or a database record that never produced
an event — is a compliance problem.

The outbox pattern solves it without distributed transactions. Instead of publishing
to the message bus directly, the application writes the event to an `outbox` table in
the same database transaction as the domain data. A separate process — a background
job or a change-data-capture pipeline — reads the outbox and publishes to the bus.
If the initial transaction fails, neither the domain data nor the outbox row lands.
If the publish fails, the outbox row is still there and the publisher retries. The
message bus sees at-least-once delivery; consumers handle deduplication using the
idempotency patterns from May.

The trade is complexity: you now have two tables, a background publisher, and CDC or
polling to think about. For services where "the event must be sent if and only if the
data was committed" is a real requirement, the outbox is the right complexity to
accept. For services where a best-effort publish is fine, it's unnecessary. Knowing
the difference is the design decision.

*Update: Worth noting the tooling options for the publisher process. Debezium, connected to SQL Server's change data capture feature, is the production-grade CDC path — it reads the transaction log and emits outbox rows as Kafka events with exactly-once guarantees at the CDC layer. For teams without a Kafka cluster, a simple polling job on a short interval (using `SELECT TOP 100 WHERE processed = 0 ORDER BY created_at`) and an `UPDATE processed = 1` after publish works at lower volume and is easier to operate. We're using polling for now; CDC is the direction when throughput requires it.*
