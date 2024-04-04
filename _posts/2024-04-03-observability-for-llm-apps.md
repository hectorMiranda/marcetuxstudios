---
layout: post
title: "Observability for LLM apps is different"
date: 2024-04-03
author: marcetux
tags: [llm, observability, ai, architecture, monitoring]
---
A consulting client asked me to add monitoring to their LLM-powered workflow and I
realized quickly that the standard application-observability playbook — latency,
error rate, saturation — captures the infrastructure story but misses the product
story entirely. A request that returns 200 OK in 400ms can still be completely wrong
and the dashboard would look fine. That mismatch is the core problem.

What LLM apps need is eval-in-production, not just metrics. For every response, you
want to capture the prompt, the retrieved context if RAG is involved, and the output
— and then run a lightweight judge pass on whether the output satisfies the intent.
The judge can be a smaller model, a rule-based check, or a human sample. The key
is that "correct" is a first-class metric alongside latency. I set up structured
logging for every LLM call — prompt tokens, completion tokens, model, latency, plus
a score field — and piped it into a dashboard. The score field starts empty; you
fill it in from eval runs.

The second thing that's different: prompts are part of the system and need version
control. I've seen projects where the prompt lives in a config file with no history.
When a model update changes behavior, they don't know which combination of
prompt-version and model-version was working. Treat prompts like migrations — dated,
versioned, tested before rollout. The infra is the easy part; the prompt history is
what saves you at 2 a.m.
