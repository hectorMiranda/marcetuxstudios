---
layout: post
title: "Idempotency keys in payment and financial APIs"
date: 2018-11-03
author: marcetux
tags: [api, architecture, banking, enterprise, reliability]
---
The first integration with the payment processing service surfaced the idempotency
requirement in the most concrete way possible: a network timeout on the client side
doesn't tell you whether the server processed the request or dropped it. In a
non-financial API, retry and see — the worst outcome is a duplicate log entry. In a
payment API, retry without an idempotency mechanism means a potential duplicate charge,
which is a customer complaint and a compliance event.

The pattern is standard: the client generates a UUID and sends it as an
`Idempotency-Key` header. The server stores the key and the outcome before returning.
On a retry with the same key, the server returns the stored outcome without re-executing
the operation. The window for key retention — how long you honor a key — is a business
decision; we landed on 24 hours, which covers any plausible retry window without
storing keys indefinitely.

The implementation detail that trips people up is the scope of "same outcome." If the
first request succeeded and you return the stored response, the status code should be
200, not 201, because the resource was not created *this time*. Some stripe-influenced
codebases return the original status code, and I've seen both; pick one and document
it in the API spec. The contract with the consumer must be explicit about what the
retry response looks like, because the consumer's retry logic will depend on it.
