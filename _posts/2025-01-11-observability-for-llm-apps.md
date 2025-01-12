---
layout: post
title: "Observability for LLM apps is not the same as observability for APIs"
date: 2025-01-11
author: marcetux
tags: [observability, llm, ai, monitoring, platform]
---
A client had an LLM-backed feature shipping to production and wanted dashboards. I
wired up the usual stack — latency histograms, error rates, throughput — and handed
them over. Two weeks later they asked why the feature was "degrading" when every metric
looked green. The latency was fine, the error rate was zero, and the outputs had quietly
gotten worse. That's a new failure mode.

The thing that makes LLM observability different is that "correct" is not a binary you
can instrument. A 500 status code is unambiguous; a response that's grammatically
perfect and subtly wrong isn't a metric, it's a judgment. So the instrumentation has to
capture *inputs and outputs*, not just the transport. That means logging prompts,
completions, and any retrieved context — enough to replay a request and evaluate it
against the same criteria you used during development. Without that, you're measuring
the plumbing and ignoring the water quality.

The stack I've settled on: emit spans with prompt/completion text attached, route them
to something queryable (even just structured logs in a searchable system), and build a
lightweight eval that scores a sample on the dimensions that matter for the specific
feature. It's more like QA infrastructure than classic APM, and it needs to be designed
in from the start, not bolted on when something goes wrong.
