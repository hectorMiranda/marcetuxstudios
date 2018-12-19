---
layout: post
title: "Distributed tracing and making a service call visible end to end"
date: 2018-12-18
author: marcetux
tags: [observability, tracing, azure, aspnet, architecture]
---
The Account Information API calls three downstream services to assemble a response.
When the 95th percentile latency spiked in November, the logs told me that something
was slow but not which service call was the culprit — each service logs its own
request time, but there's no way to correlate them to a single upstream request without
the request ID propagating through every hop. That's the distributed tracing problem,
and the deployment freeze is a good time to design the solution.

The concept is straightforward: every incoming request gets a trace ID if it doesn't
already have one, and every downstream call propagates that trace ID in a header. Each
service records a span with its own execution time, tagged with the trace ID. A trace
aggregator (we're evaluating Azure Application Insights and Zipkin) collects the spans
and renders the call tree. You see the total request duration and which span consumed
which fraction of it.

The implementation in ASP.NET Core 2.1 is a middleware and an `HttpClient`
`DelegatingHandler` that read and write the W3C Trace Context headers — the specification
became a W3C candidate recommendation this year. The middleware is twelve lines. The
value is that the next latency spike will have a call tree, not a collection of
disconnected log entries. Observability is not a feature you retrofit — it's a
structural property you build in before you need it.
