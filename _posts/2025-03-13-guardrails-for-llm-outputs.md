---
layout: post
title: "Guardrails for LLM outputs, not just the inputs"
date: 2025-03-13
author: marcetux
tags: [llm, guardrails, ai, safety, platform]
---
The guardrails conversation in most teams I talk to is about inputs — filtering what
users can send to the model. That matters, but it's the easier half. The harder half
is what you do with what comes back out. An output that's factually wrong, brand-
unsafe, or structured incorrectly can reach the user even if the input was perfectly
clean, and a content filter on the request won't catch any of it.

The output guardrails worth building are tiered. The cheapest is structural validation:
if your prompt asks for JSON, validate that you got JSON before passing it downstream.
Schema mismatch is the most common category of production breakage I see, and it costs
nothing to catch — parse and validate before you trust. The next tier is a semantic
check: does the output make sense given the input? This can be a simple classifier,
a regex against a blocklist, or a secondary model call on the output. The cost goes up
but so does what you catch.

The one to invest in for anything customer-facing is factual grounding — if your
feature claims to have retrieved information, did the answer actually come from the
retrieved documents? A simple string-match overlap between the output and the retrieval
context catches a surprising fraction of hallucinations without a model call. The goal
isn't perfection; it's a ratchet that makes the failure modes explicit and measurable
rather than invisible.
