---
layout: post
title: "Structured logging that Splunk can actually use"
date: 2019-01-19
author: marcetux
tags: [observability, splunk, logging, dotnet, architecture]
---
The bank runs Splunk for log aggregation, which means every log line I write is eventually a search query someone runs at 2 AM during an incident. I spent years writing logs for myself as a developer; I've been relearning to write them for the person who's never seen my code, searching across a million lines, trying to reconstruct what happened.

The shift is from freeform strings to structured key-value pairs. Instead of `"Payment processed for customer 12345 in 142ms"`, it's `{ "event": "payment.processed", "customerId": 12345, "durationMs": 142, "correlationId": "..." }`. Serilog makes this natural in .NET — you write `Log.Information("Payment processed for {CustomerId} in {DurationMs}ms", id, ms)` and it logs both the rendered string and the named properties as a JSON object. Splunk indexes the fields, and now `customerId=12345 earliest=-1h` is a real query, not a regex against text.

The other discipline is correlation IDs. Every request at the gateway gets one assigned if it doesn't arrive with one, and every log line in every downstream service carries it. When a complaint comes in about a specific transaction, I search on the correlation ID and get the full trace across services — what the gateway did, what the payment service decided, what the audit log recorded — in timestamp order. That's the difference between "we had an error" and "here is exactly what happened and why." Structured logs turn Splunk from a dumpster into a tool.
