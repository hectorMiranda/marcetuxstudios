---
layout: post
title: "Distributed tracing without the overhead"
date: 2024-05-25
author: marcetux
tags: [observability, tracing, opentelemetry, architecture, distributed-systems]
---
A consulting client had a microservices system with no tracing and they were
convinced that adding it would require a full OTel overhaul with a collector, a
backend, the works. I told them to start smaller. What they needed wasn't full
distributed tracing — they needed to be able to follow a single request across
three services without reading logs from three dashboards simultaneously. That's a
narrower problem.

The minimal version: propagate a correlation ID from the edge into every downstream
call. The ID goes in an HTTP header, gets extracted in middleware, gets attached to
every log line. Now `grep` on that ID in aggregated logs gives you the request's
full journey across services. No OTel, no collector, no new infrastructure. This
takes an afternoon and solves 80% of the "I can't figure out where the request
went wrong" complaints. I've done this in five different systems and it immediately
improves production debugging even before anything fancier lands.

Then, once you've felt the pain of the other 20% — timing information, async spans,
fan-out tracking — add OTel instrumentation deliberately, service by service. OTel
is the right long-term answer; it's also more infrastructure than most teams need
on day one. The mistake is treating it as all-or-nothing. Correlation IDs first,
structured logging second, span propagation third. Each step delivers value; none
of them require the next one to work.
