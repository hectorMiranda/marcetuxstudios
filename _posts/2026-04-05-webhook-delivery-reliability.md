---
layout: post
title: "Webhook delivery reliability is an infrastructure problem"
date: 2026-04-05
author: marcetux
tags: [api, webhooks, reliability, distributed-systems, dotnet]
---
AmaWaterways sends identity events — user provisioned, permissions changed, account suspended — to downstream systems via webhooks. The downstream systems are a mix of internal services and third-party booking platforms, and their reliability ranges from "always available" to "down for maintenance at the exact wrong moment." This is the part of webhook design that articles about webhook *sending* consistently underemphasize: delivery is not a fire-and-forget operation, and treating it as one pushes the reliability problem onto receivers who can't solve it.

The pattern that works is an outbox: instead of calling the webhook endpoint inline with the operation that triggered the event, write the event to a delivery queue as part of the same database transaction. A separate worker process picks events off the queue, attempts delivery with exponential backoff, and marks events as delivered or permanently failed after a configurable retry count. The event is durable as soon as the transaction commits; the delivery is a best-effort with logging and alerting on repeated failures. The application never blocks on an external HTTP call, and the queue provides a complete picture of what was attempted and what failed.

The receiver side of this is worth thinking about explicitly: the downstream system should expect duplicate deliveries and treat them idempotently. The webhook payload includes an event ID, and any idempotent receiver checks whether it's already processed that ID before doing anything. This symmetry — idempotent sender via outbox, idempotent receiver via deduplication — is what makes the overall system reliable rather than just eventually correct under favorable conditions.
