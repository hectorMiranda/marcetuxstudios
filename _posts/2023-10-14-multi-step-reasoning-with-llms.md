---
layout: post
title: "Multi-step reasoning with LLMs, the engineering side"
date: 2023-10-14
author: marcetux
tags: [llm, ai, reasoning, agents]
---
The interview loop I'm in right now involves a lot of questions about "agentic"
systems — LLM-powered pipelines that break a task into steps, execute tools, observe
results, and continue. The question underneath them is: how do you make multi-step
reasoning reliable? Which is a fair question because the naive implementation makes it
fragile.

The failure mode is compounding errors. Each step's output is the input to the next;
a hallucination in step 2 propagates to step 3, which propagates to step 4, and the
final answer is confidently wrong in a way that's hard to trace back. The mitigation
that actually helps is tool calls with ground truth: instead of asking the model to
"reason about" whether two values are equal, give it a calculator tool and let it call
it. Instead of asking it to "recall" a database value, give it a query tool. Ground
the factual steps in deterministic lookups; let the model handle the reasoning steps
that connect them.

The other mitigation is short chains. Every additional step multiplies the error
probability. A 5-step chain with 90% per-step accuracy has a 59% chance of getting
all five steps right. A 3-step chain with 90% accuracy has 73%. Design the task
decomposition to use as few model-generated steps as possible, and make the steps as
independent as they can be. It's the microservices lesson applied to inference chains:
dependencies that compound are the enemy.
