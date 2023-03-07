---
layout: post
title: "GPT-4 released, notes from the first day"
date: 2023-03-06
author: marcetux
tags: [llm, gpt4, ai, tooling]
---
OpenAI released GPT-4 today and I've been at the API for six hours. The headline
claim is better reasoning, longer context (8k tokens in the standard version, 32k for
the preview tier), and measurable improvements on professional benchmarks. The part
that matters for what I'm building: it handles technical code with noticeably fewer
hallucinated library calls, and multi-step reasoning problems that tripped GPT-3.5 up
come back coherent.

The concrete test I ran: feed it a description of the Casper deploy state machine from
my February post and ask it to implement the polling loop in Rust. GPT-3.5 wrote
syntactically valid Rust that called a method that doesn't exist in the SDK. GPT-4
wrote code that was wrong in a more interesting way — it missed the "check execution
result, not just finality" nuance — but the code itself was structurally sound and I
fixed it in two minutes. Smaller gap to close.

The 32k context window in the preview tier is the thing I'm most interested in. That's
long enough to stuff a reasonable chunk of a codebase into a single prompt — not as a
replacement for thinking, but as a way to ask questions about unfamiliar code without
switching tools constantly. My instinct is that context length matters more than model
size for most real engineering tasks. Longer window = more of the situation fits.
