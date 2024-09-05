---
layout: post
title: "Guardrails for LLM apps in production"
date: 2024-09-04
author: marcetux
tags: [llm, ai, security, production, architecture]
---
A client asked me what guardrails I'd put on an LLM-powered customer support system
before putting it in front of real customers. The question is good; the timing
surprised me because they were two weeks from launch with nothing in place. This is
more common than it should be: teams build the capability, demo it extensively, and
ship it before they've thought about what happens when a user tries to do something
the demo never showed.

The baseline I've settled on: input validation, output validation, and rate limiting.
Input validation catches the obvious injection attempts and nonsense inputs before
they hit the model at all — it's a token-budget decision as much as a safety one.
Output validation checks that the response is actually in the domain the system is
supposed to handle; a customer support bot that confidently answers a political
question is a PR problem waiting to happen. Rate limiting prevents both abuse and
runaway costs when someone writes a loop. None of these are sophisticated; all of
them are missing from first deployments more often than they should be.

The harder guardrail is scope enforcement — ensuring the system answers questions in
its domain and declines others gracefully. A system prompt with clear scope works
for most cases. A classifier that labels off-domain requests before they reach the
generation step is more reliable. Which you use depends on how badly a mis-scoped
answer hurts. For financial or medical adjacent domains: the classifier. For a
general assistant: the prompt is usually enough. Know the cost of the failure before
you pick the control.
