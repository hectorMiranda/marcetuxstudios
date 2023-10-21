---
layout: post
title: "Interviewing for AI engineering roles in 2023"
date: 2023-10-20
author: marcetux
tags: [career, llm, ai, job-search]
---
Three weeks into the active search and I have enough data to see the shape of the
market. The roles divide into three categories: companies that genuinely understand
what they're building, companies that know they want "AI" and are figuring it out, and
companies that are bolting an LLM endpoint onto an existing product and calling it a
strategy. The interviews are different in each category.

The first category asks technical questions about retrieval architecture, eval
frameworks, latency vs quality tradeoffs at inference time, and how you'd instrument
an LLM pipeline for observability. Those are good questions because they have real
answers and the answer changes based on the constraints. Category two asks broader
questions about design — "how would you build a document Q&A system?" — and wants to
hear your reasoning process. That's fine; the reasoning is real. Category three asks
what model I'd use and whether I've used the OpenAI API. The answer to both is yes,
but neither is the interesting part.

I'm filtering for the first category with tolerance for the second. The Rust background
is coming up more than I expected — two interviews specifically asked about it for
local inference performance work. The evening portfolio is pulling weight in every
conversation. Building the thing is still the best way to interview for the job of
building the thing.
