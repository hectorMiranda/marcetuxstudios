---
layout: post
title: "Agent patterns that actually work so far"
date: 2024-03-02
author: marcetux
tags: [ai, agents, llm, architecture, patterns]
---
Everyone is building agents in early 2024 and most of them are failing in the same
way: the loop runs three turns, the model loses track of its goal, and the output
is a confidently stated hallucination. I've been on two consulting engagements now
where the agent architecture was the problem, not the underlying model capability.

The pattern that works is keeping the agent's scope narrow and its state explicit.
An agent that can do anything ends up doing nothing reliably. One that has three
tools — search, read, summarize — and writes its intermediate findings to a shared
scratchpad before each new step, is debuggable. When it goes wrong you can read the
scratchpad and see exactly where the reasoning broke. When it goes right you have a
log of why. The scratchpad idea feels obvious but I've seen it absent in most first
implementations.

The second thing that works: human-in-the-loop for consequential actions. Write to
a database, send an email, deploy something — those should surface for confirmation
before executing. Not because the model can't get it right, but because the cost of
one wrong consequential action outweighs the friction of one confirmation. The agent
that runs fast and unsupervised is impressive in a demo. The one with a checkpoint
before irreversible actions is the one you let near production.
