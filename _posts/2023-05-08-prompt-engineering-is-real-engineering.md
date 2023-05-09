---
layout: post
title: "Prompt engineering is real engineering, sort of"
date: 2023-05-08
author: marcetux
tags: [llm, ai, prompting, tooling]
---
The phrase "prompt engineering" gets dismissed in some corners as not real work —
tweaking text inputs doesn't deserve the word "engineering." After building three
different RAG pipelines this year, I'm going to push back on that, carefully.

The real work in prompting is constraints and structure. A vague prompt gets a vague
answer; a prompt that specifies the output format, the reasoning constraints, what to
say if uncertain, and what sources to cite gets a predictable, parseable response.
That specification process — anticipating the model's failure modes and encoding
guardrails into the prompt — is closer to API design than creative writing. You're
designing a contract with a probabilistic system.

What it's not: prompt engineering is not a substitute for understanding the model's
actual capabilities and limits. "Jailbreak"-style tricks that get the model to do
something it's supposed to refuse, elaborate chain-of-thought templates that only work
on one model version, brittle regex on the output — these are hacks, not engineering.
The prompts that hold up are the ones that work because they help the model understand
the problem, not because they found a pattern that confuses the safety filter. That
distinction is worth maintaining.
