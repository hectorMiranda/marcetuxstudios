---
layout: post
title: "Agentic coding, six months in"
date: 2024-11-07
author: marcetux
tags: [ai, agents, copilot, coding, tooling]
---
I've been using agentic coding tools — the kind that can write files, run tests,
iterate on failures — since early 2024 and I have enough hours to say something real
about where they fit in a senior engineer's workflow. The short version: they're fast
at things I already know how to do and slow at things I don't. That's almost exactly
wrong for the leverage you'd want from a tool.

The long version: for well-scoped tasks in a codebase I'm familiar with — write a
test for this function, refactor this class to this interface, generate a migration
for this schema change — the agentic loop is faster than doing it manually. The agent
knows the codebase context, can run the tests, can iterate on failures. I review the
output and the delta is real. For things I'm trying to figure out — how to structure
an unfamiliar API, what the right architecture is for a new constraint — the agent
generates fast and confidently and I verify slowly. The verification takes longer
than doing it myself from scratch.

The tool that's changed my workflow most is the context-aware completion that reads
the current file and the surrounding repo structure, not a specific agentic product.
Fast, accurate, within my workflow. The full agentic loop has value for specific,
scoped tasks with clear success criteria. For open-ended engineering it produces work
I have to carefully validate. That validation is not nothing; it's still my job.
