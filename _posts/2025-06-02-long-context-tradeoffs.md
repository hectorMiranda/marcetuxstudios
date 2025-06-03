---
layout: post
title: "Long context windows and when not to use them"
date: 2025-06-02
author: marcetux
tags: [llm, ai, architecture, context, performance]
---
Context windows have grown to the point where the tempting answer to any "how do I get
relevant information to the model" question is just "put everything in the context." A
whole codebase, a full document library, a year of conversation history — technically
feasible now. The question I keep asking is whether it's the right answer for the
specific problem, and more often than not it isn't.

The issue is not capability, it's cost and latency. A 200K token context costs
proportionally more than a 5K context, and it's slower. For an interactive feature
where the user expects a response in under two seconds, a maxed-out context is working
against you. For a batch process that runs overnight, it's a different calculus. The
context size decision is a tradeoff that depends on your workload's latency and cost
tolerance, not a question of what the model *can* handle.

The retrieval vs. context tradeoff has a more nuanced answer in 2025 than it did in
2023. Then, retrieval was necessary because the context was small. Now, retrieval is
still often better because it's faster and cheaper, even when long context would work.
The old "retrieval to fit" framing has evolved into "retrieval to focus" — the best
retrieval surfaces the relevant 2% so the model doesn't have to work through the other
98% to find it. Cost, latency, and recall; pick the system that optimizes all three.
