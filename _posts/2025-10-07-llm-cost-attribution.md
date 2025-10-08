---
layout: post
title: "LLM cost attribution in production"
date: 2025-10-07
author: marcetux
tags: [llm, ai, cost, observability, platform]
---
Token costs for LLM features are significant enough that teams need to attribute them
to the features generating them, not just track the total monthly bill. The pattern I
see in most teams: one API key for the whole application, one line item on the invoice
at the end of the month, and a conversation about why the bill went up that no one can
answer with data.

The fix is attribution at the span level. Every LLM call gets a span with the feature
name, the use case, and the token counts as attributes. Aggregate those spans by feature
and you have the breakdown. This is the same observability infrastructure you'd build
for latency; you're adding cost as another dimension. The token counts come back in the
API response; you're already receiving them, you just need to record them somewhere
queryable.

The decision that follows from having this data: which features are expensive enough to
warrant optimization, and which ones are cheap enough to leave alone. A feature that's
doing 500K tokens a day for routing and classification is worth migrating to a smaller
local model. A feature that's doing 2K tokens a day for a high-stakes use case is fine
on the frontier model. Without attribution you're applying the same optimization pressure
to both, which means you're optimizing the wrong one or both are too expensive. The data
makes the prioritization obvious.
