---
layout: post
title: "What agentic coding actually looks like after a month"
date: 2025-01-03
author: marcetux
tags: [ai, agents, tooling, workflow, consulting]
---
December I started wiring LLM-backed agents into client delivery work and expected the
standard "impressive demo, unreliable in practice" result. What I got was something
messier and more useful: the agents are unreliable in *predictable* ways, which turns
out to be most of what you need to make them worth running.

The pattern that stuck is treating the agent as a junior that needs its scope bounded.
Give it an entire codebase and a vague task and it hallucinates paths and writes
confident nonsense. Give it a single file, a concrete output contract, and a way to
verify its own work — a test to run, a schema to validate against — and it completes
the mechanical part of the task correctly most of the time, freeing me to focus on
whether the mechanical part was even the right thing to do. The tool is good at
execution once you've done the thinking.

The consulting angle is that I bill on judgment, not keystrokes, so any speed I get on
the keystrokes is pure gain. January resolution: build a repeatable scaffold for
agent-assisted consulting work — task decomposition, verification loops, a short
feedback cycle. No magic, just a workflow I can explain to a client without
embarrassment.
