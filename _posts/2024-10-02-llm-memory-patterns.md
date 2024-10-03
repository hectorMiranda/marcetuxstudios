---
layout: post
title: "Memory patterns for stateful LLM applications"
date: 2024-10-02
author: marcetux
tags: [llm, ai, memory, architecture, patterns]
---
Stateful LLM applications — things that feel like they remember you across sessions
— don't use magic. They use one of three patterns for representing prior state, and
the choice determines the quality, cost, and privacy story of the application. I've
built each of these in the past year and the differences are more consequential than
they look in a design review.

The simplest: the full conversation transcript in the context window. Every prior
turn, verbatim. Works perfectly until the context window fills, costs more per
request as the session grows, and transfers a copy of every prior message to the
model provider on each request. Fine for short sessions; wrong for anything with a
privacy constraint or longer than a few dozen turns.

The second: rolling summary. A background step periodically compresses older turns
into a summary paragraph and drops the raw turns. The context window stays bounded;
some nuance is lost in compression. For most applications the nuance loss is
acceptable. For high-stakes applications where exact prior statements matter, it
isn't. The third: semantic retrieval. Prior turns are stored in a vector index;
the current query retrieves the most relevant prior context, not the most recent.
This scales arbitrarily and retrieves exactly what's relevant, at the cost of
retrieval latency and the risk that something important isn't retrieved because it
isn't semantically similar to the current query. Match the pattern to the use case.
None of them is universally right.
