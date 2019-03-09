---
layout: post
title: "Distributed tracing with Application Insights"
date: 2019-03-08
author: marcetux
tags: [observability, azure, tracing, dotnet, architecture]
---
Correlation IDs in structured logs get you a lot, but reading logs across services in time order is still manual — you search, filter, sort, and mentally reconstruct a sequence. Distributed tracing automates that reconstruction. I got Application Insights wired into three services this month and the trace waterfall view made a genuinely slow integration problem visible in minutes that had taken days to even characterize.

The model is spans: each unit of work — an HTTP call, a database query, a Service Bus receive — creates a span with a start time and a duration. Spans nest into a trace tree under a root span with the trace ID that flows across service boundaries. When a request arrives, the gateway reads the incoming trace context (we use the W3C `traceparent` header), propagates it downstream, and every service adds its spans to the same trace. App Insights collects all of them and assembles the waterfall without any manual correlation.

The practical upside: the slow payment we'd been chasing turned out to be a loop making N+1 calls to the account service — obvious in the trace waterfall, invisible in the logs unless you knew to count. Fixing the batching dropped the p99 latency by sixty percent. The discipline is keeping sampling sane: tracing every request in high-volume services fills storage fast, so we sample at ten percent in normal operation and keep one hundred percent for error paths. You want the full trace for every failure; you just don't need it for every success.
