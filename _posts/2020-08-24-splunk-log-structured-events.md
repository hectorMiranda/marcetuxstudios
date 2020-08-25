---
layout: post
title: "Structured logging and why Splunk gets better when you help it"
date: 2020-08-24
author: marcetux
tags: [observability, splunk, dotnet, logging]
---
Half the services on our cluster log like this: `"Payment processed: 12345"`. The
other half log like this: `"Payment processed" paymentId=12345 amount=1250.00
customerId=98765 duration_ms=43`. The first style is for humans reading a file. The
second is for a machine indexing events. Splunk can search both, but the structured
events let you write queries that would be impossible on the freeform string.

Serilog with its `{PropertyName}` template syntax is the .NET answer. The call
`Log.Information("Payment processed {PaymentId} {Amount}", id, amount)` emits a
structured event — Splunk's ingestion pipeline receives the property names and values
as distinct indexed fields. Now the query is `index=banking source=payment-service
PaymentId=12345` instead of a regex against a freeform string. Or `stats avg(duration_ms)
by source` across every service that logs a duration. The aggregations that make
Splunk useful depend on the fields being fields, not substrings.

The migration strategy I've been pushing: don't refactor old log statements, just
write new ones structured. When you touch a file for a real reason, update the log
calls in that file. Over a quarter, the structured percentage rises without making
"log refactoring" its own project. Start with the on-call paths — the log lines
you actually search — and let coverage grow from there. The difference in a 2 AM
incident between searching a field and grepping a string is worth the week you spend
getting there.
