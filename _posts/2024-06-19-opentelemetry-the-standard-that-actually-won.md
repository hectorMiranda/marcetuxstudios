---
layout: post
title: "OpenTelemetry is the observability standard that actually won"
date: 2024-06-19
author: marcetux
tags: [opentelemetry, observability, tracing, metrics, architecture]
---
I've been through several observability generations: hand-rolled correlation IDs,
Zipkin, Jaeger, Datadog APM, and various proprietary agents. OpenTelemetry is the
first approach where I'm confident the instrumentation won't need to be ripped out
when the backend changes. The reason: OTel separates instrumentation from export.
You instrument once, you configure exporters separately, and the same traces can go
to Jaeger today and a commercial backend tomorrow without touching application code.

The SDK is opinionated in the right places. A tracer, a meter, a logger — the three
signal types — each with a standard API and a pluggable backend. The auto-
instrumentation packages for common frameworks (ASP.NET, Django, FastAPI, gRPC)
add spans without any code change. The manual instrumentation API for custom spans
is a few lines and works the same way in every language that has an SDK. I've added
OTel to three consulting engagements this year and the time-to-first-trace is now
measured in hours, not days.

The one rough edge: the metrics pipeline is younger than the tracing pipeline. The
Prometheus compatibility story is good; the push-based metrics export configuration
has more surface area than it should for common cases. But this is moving fast — the
OTel collector gets better every release — and the stability direction is clearly
right. If you're adding observability to a new system in 2024, start with OTel. The
alternative is picking a vendor's proprietary SDK and re-doing this work when the
contract doesn't renew.
