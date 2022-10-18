---
layout: post
title: "gRPC health checks for the Casper node monitoring stack"
date: 2022-10-17
author: marcetux
tags: [grpc, devops, monitoring, casper, rust]
---
Running a fleet of Casper nodes for testing means knowing which ones are healthy without
polling each one's application-level API constantly. gRPC has a standard health check
protocol — defined in the grpc-health-v1 protobuf — and wiring it into our node
monitoring stack this month replaced a collection of shell scripts with a consistent,
checkable interface.

The gRPC health service exposes a `Check` RPC that returns `SERVING`, `NOT_SERVING`,
or `SERVICE_UNKNOWN` for a named service. The convention is to register your services
by name and implement the status checks against whatever your definition of "healthy"
means — for a Casper node, that's whether the SSE event stream is connected, whether
the node is behind on blocks, and whether the RPC endpoint is responding. Our
Prometheus metrics exporter now calls `Check` on each node and exposes the result as
a gauge metric, so the Grafana alerting rules can fire on `SERVING` transitions.

The watch the health service serves is worth mentioning: `Watch` streams status changes
as they happen, rather than requiring periodic polling. That's the gRPC streaming
pattern applied to health checks — instead of scraping every thirty seconds and missing
a thirty-second outage, we hold an open stream and react immediately to status changes.
Small architectural choice, significant improvement in detection latency. The scrape
interval is always a bet against how long you're willing to be blind.
