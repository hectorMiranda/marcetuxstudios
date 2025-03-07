---
layout: post
title: "Agent tool-use patterns that actually hold up"
date: 2025-03-06
author: marcetux
tags: [ai, agents, mcp, tooling, architecture]
---
Three months into shipping agent-assisted features on client engagements and the
patterns that looked promising in January have mostly held up. The one I reach for most:
give the agent a small, closed set of typed tools, not a broad "do anything" capability.
An agent with five tools that each do one thing well is more reliable than one with
fifteen that blur together. When the model has to choose between "update record" and
"patch record" and "modify record," it makes the wrong choice often enough to matter.
Name your tools like you name your functions — one clear verb, one clear noun.

The failure mode I underestimated is retries on partial success. If a multi-step agent
workflow fails midway, what does step two look like when step one already ran? This is
the idempotency problem, familiar from API design, now showing up in agent orchestration.
The answer is the same: design each tool call to be safe to replay. If you can't make a
tool idempotent, put a checkpoint before it and don't retry blindly past the checkpoint.

The third thing that aged well is verification at every step rather than once at the
end. Agents that check their own output against the spec before moving to the next step
catch more errors than agents that barrel through and validate at completion. It costs
extra tokens and latency; it saves human review time. Usually the right trade.
