---
layout: post
title: "OpenTelemetry semantic conventions for LLM apps"
date: 2025-05-06
author: marcetux
tags: [opentelemetry, observability, llm, ai, platform]
---
In January I wrote about the principle of capturing prompt and completion text in spans.
In May I have a more concrete recommendation: the OpenTelemetry project has published
semantic conventions for GenAI systems, and adopting them pays off immediately in
interoperability. Tools that consume OTel data and understand the GenAI conventions can
render meaningful dashboards and alerts without custom configuration. Inventing your
own attribute names means inventing your own dashboards.

The conventions cover the things you actually want to observe: model name, request
token counts, response token counts, finish reason, top-level prompt and completion
capture, and error classification. The attribute names are normalized — `gen_ai.system`,
`gen_ai.request.model`, `gen_ai.usage.input_tokens`, and so on. Once your
instrumentation emits these names, any OTel-aware backend can index and query them
without a custom parser.

The implementation detail worth knowing: prompt and completion text go on events
attached to the span, not as span attributes. Events are lower-cost and don't pollute
the attribute index with large strings. This is the spec's recommendation and it makes
the observability backend happier at scale. Wire the conventions in from the start; it's
a lot easier than migrating attribute names after you have production data in a backend
that's already learned your custom schema.
