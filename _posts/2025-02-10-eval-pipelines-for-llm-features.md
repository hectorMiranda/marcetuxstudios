---
layout: post
title: "Eval pipelines for LLM features before they hit production"
date: 2025-02-10
author: marcetux
tags: [llm, evals, testing, ai, platform]
---
The January observability post was about watching what's happening in production. This
one is about building the feedback loop that runs before anything ships. The gap I keep
finding in client codebases is that LLM features have unit tests for the surrounding
code and exactly zero automated quality signal on the model outputs themselves.

An eval pipeline is not complicated in principle: assemble a set of input cases with
expected outputs or rubrics, run the feature against them, score the results, and fail
the build if the score drops below a threshold. The execution detail is that "score"
for language model outputs is rarely binary — you need a rubric, and the rubric is
usually best captured as a small evaluator that itself uses a model. LLM-as-judge has
become a real pattern, with the caveat that the judge model needs to be more capable
than the model being judged, and the rubric needs to be specific enough that you'd
agree with the judgment on a sample you scored by hand.

The pragmatic starting point: take ten real examples from production logs, score them
by hand, and write an evaluator that agrees with your scores on eight of them. That's
your eval. Run it on every PR that touches the prompt or the retrieval chain. You're
not solving measurement theory; you're building a ratchet that catches regressions.
