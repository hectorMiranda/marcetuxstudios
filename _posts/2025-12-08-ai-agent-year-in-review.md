---
layout: post
title: "The AI agent year in review"
date: 2025-12-08
author: marcetux
tags: [ai, agents, mcp, consulting, retrospective]
---
At the start of 2025 I was describing agentic workflows as "unreliable in predictable
ways." By December I'm describing them as reliable for the use cases they're suited to,
which is a meaningful shift. The technology didn't change that dramatically — the models
improved incrementally, the tooling got more stable — but the pattern language matured,
and the gap between "demos that impress" and "systems that run" narrowed significantly.

The patterns that held up: bounded tools, typed interfaces, verification at each step,
idempotent operations, comprehensive logging. The patterns that didn't: agents with
unrestricted capability, trust placed in model outputs without validation, orchestration
that relies on in-memory state rather than durable storage. None of this is specific to
AI systems; it's distributed systems engineering applied to a new substrate.

The thing I keep telling clients at year-end: the agent work that ships in 2026 will be
normal software engineering, not a specialized discipline. The patterns will be in every
framework, the observability will be in every stack, and the engineers who spent 2025
building and debugging agentic systems will be the ones who can move fast because
they've already made the mistakes. The experimentation tax has been paid; what's left
is applying what we learned.

*Update: worth adding — the eval tooling for agents also matured meaningfully in 2025.
A year ago most teams were evaluating by hand or not at all. The pattern of running
automated evals in CI against a curated test set is now common enough that I encounter
it in client codebases without having introduced it. That's a good sign: the discipline
is spreading on its own.*
