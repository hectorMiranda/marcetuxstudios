# ADR 0012 — Azure Service Bus for asynchronous integration

**Status:** Accepted  
**Date:** 2018-10-04

## Context

Several integration flows between the API layer and downstream core-banking systems
are long-running (>30s) and must not block client requests. The alternatives evaluated
were: synchronous HTTP with a callback URL, RabbitMQ (self-hosted), Azure Storage
Queues, and Azure Service Bus.

## Decision

Use Azure Service Bus Standard tier with topics and subscriptions for all
asynchronous integration messaging.

## Rationale

- Service Bus is the approved Azure messaging service in the bank's cloud standards.
- Topics with subscriptions support the publish-subscribe pattern required when multiple
  systems must react to the same event (e.g., account status change).
- Dead-letter queues and lock renewal are first-class features, not bolt-ons.
- Managed identity authentication eliminates connection-string rotation.
- Storage Queues lack topic/subscription semantics. RabbitMQ adds an operational
  dependency the infrastructure team is not resourced to support.

## Consequences

- All services in async flows must handle at-least-once delivery (idempotent message
  processing is required).
- The team owns the Service Bus namespace configuration as infrastructure-as-code
  (ARM templates).
- Dead-letter monitoring must be included in every queue's alert definition.
