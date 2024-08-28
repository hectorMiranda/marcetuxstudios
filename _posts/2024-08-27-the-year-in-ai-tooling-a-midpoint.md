---
layout: post
title: "The year in AI tooling, a midpoint check"
date: 2024-08-27
author: marcetux
tags: [ai, llm, retrospective, tooling, meta]
---
Eight months in and I want to take stock of where the AI tooling actually is versus
where I expected it to be in January. The expected things: RAG is the workhorse of
enterprise deployments, fine-tuning is narrower in application than the hype
suggested, local models are more capable than most people know. All true.

The surprise is how much engineering practice has consolidated. Structured output is
table stakes — nobody is parsing free text in production anymore. Function calling
is the standard integration layer. Eval frameworks are being taken seriously by teams
that were doing vibe-checks six months ago. The tooling is starting to feel like
engineering rather than research-in-production. That maturation is real and I think
it's underreported relative to the model capability news cycle.

What I didn't predict: the retrieval quality gap between what a careful implementation
produces and what a naive one produces is larger than I expected. The teams that
invested in chunking strategy, reranking, and eval have systems that feel qualitatively
different from the teams that took the default pipeline and moved on. The
infrastructure is easy; the calibration is the hard part. That's true of every layer
of engineering I've worked in for twelve years, and it turns out to be true here too.
The tool does what you configure it to do.
