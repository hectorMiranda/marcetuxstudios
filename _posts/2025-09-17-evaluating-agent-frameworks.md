---
layout: post
title: "How I evaluate an agent framework before committing to it"
date: 2025-09-17
author: marcetux
tags: [agents, ai, tooling, architecture, consulting]
---
The agent framework landscape is crowded enough in 2025 that "which one should we use"
has become a real consulting question rather than one with an obvious answer. My
evaluation process has settled into three questions that eliminate most candidates
quickly.

First: can I read the framework's source and understand what it's doing when a request
goes wrong? A framework that requires trusting its abstractions for debugging is a
framework that will cost you hours in production. I've switched clients off two
frameworks this year because the debugging story was "add logging and hope," which is
not acceptable when an agent is doing something expensive or irreversible.

Second: does the framework expose the model API directly enough that I can instrument
it? If the framework wraps model calls in ways that prevent me from attaching
observability, I'm flying blind. The frameworks that compose well with OpenTelemetry
and standard logging are the ones I trust.

Third: what's the migration story if the framework is abandoned or pivots? This sounds
pessimistic but the agent framework space is moving fast enough that "this won't be the
right choice in two years" is a realistic scenario. Frameworks where the business logic
is cleanly separated from the orchestration are the ones where migration is a project
rather than a rewrite. Prefer the one you can leave over the one you can't.
