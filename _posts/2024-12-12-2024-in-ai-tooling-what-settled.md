---
layout: post
title: "2024 in AI tooling and what settled"
date: 2024-12-12
author: marcetux
tags: [ai, llm, retrospective, tooling, rag]
---
I want to mark which practices went from "emerging technique" to "obvious baseline"
in 2024, because the pace of movement made it easy to lose track of where the field
actually is. At the start of the year, a reranker pass in a RAG pipeline was an
advanced optimization. By year end, I'm explaining it as part of the baseline in
every introductory conversation. That normalization happened inside twelve months.

What settled: structured output and JSON mode as the production standard for any
integration. Chunking at semantic boundaries rather than character counts. Eval
frameworks as a prerequisite to shipping, not an optional improvement. Function
calling as the integration layer. Batch APIs for non-time-sensitive LLM workloads.
Semantic caching for high-volume stable queries. Prompt version control. All of
these were "advanced" or "optional" in January. They're baseline now.

What's still unsettled: the agent framework layer. There are a half-dozen frameworks
and no clear winner, and the "right" level of abstraction for multi-step agent
systems is genuinely an open question. The MCP announcement is the most interesting
architectural bet of the year on this question — a protocol-level answer rather than
a framework-level one. Whether the bet pays off depends on adoption that hasn't
happened yet. Watch the MCP ecosystem in Q1. Everything else, though — RAG,
structured output, eval — is done being experimental. Build on it.
