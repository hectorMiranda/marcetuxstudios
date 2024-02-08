---
layout: post
title: "Fine-tuning and when it actually earns its cost"
date: 2024-02-07
author: marcetux
tags: [llm, fine-tuning, ai, ml, architecture]
---
A client asked whether they should fine-tune a model for their internal
documentation assistant. My first question back was whether they'd already tried
good RAG with structured prompts, because fine-tuning to fix retrieval problems is
the wrong lever. They hadn't. We spent a day improving the retrieval pipeline and
the outputs got 80% of the way there without touching the model weights at all.

Fine-tuning earns its cost in specific situations: you need a *style* or *format*
the model can't learn from instructions alone; you're making thousands of requests
where a shorter, cheaper model with task-specific tuning beats a larger general one
on latency and cost; or you have proprietary knowledge that is too large for any
reasonable context window and too sensitive for a public API. Those are real
scenarios. "The prompt isn't working" is almost never one of them.

The other thing fine-tuning buys you is consistency — a tuned model answers in the
same voice, the same structure, every time, in a way that in-context instructions
don't fully guarantee. For a product with a specific persona that matters. For an
internal tool that just needs to be correct, it usually doesn't. Use the big model
with careful prompts first. Fine-tune when you've measured the ceiling and it's
still not enough, not before.
