---
layout: post
title: "Idempotency in banking APIs"
date: 2019-01-10
author: marcetux
tags: [api, banking, idempotency, reliability, architecture]
---
A network timeout on a payment initiation call is a very different kind of ambiguity than a timeout on a report query. The report can be re-fetched; the payment might have processed and might not have — you genuinely don't know. Without idempotency you're choosing between duplicate transactions and lost ones, and neither answer satisfies compliance.

The pattern is a client-generated idempotency key — a UUID the caller mints before the first attempt and resends on every retry. The server stores the key and its associated outcome. If a second request arrives with the same key, the server returns the stored response instead of processing again. No double-debit, no confusion. The trick is defining "same outcome": for payments, the original response is replayed verbatim, including any transaction reference, because the downstream system that already processed it needs to see a consistent record.

Where it gets tricky is the key's TTL and what "processing" means for an async flow. We queue payments rather than processing synchronously, so the accepted response is returned on the first call, and if the same key arrives while the original is still queued we return the same 202 with the same Location header. If the key arrives after completion, we return the final status. It's a little finite-state-machine behind every idempotent endpoint, and the state has to survive a service restart. Boring to design; absolutely worth it when a mobile client double-taps.
