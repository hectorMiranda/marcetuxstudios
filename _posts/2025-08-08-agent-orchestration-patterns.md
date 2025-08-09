---
layout: post
title: "Agent orchestration patterns for multi-step workflows"
date: 2025-08-08
author: marcetux
tags: [agents, ai, architecture, orchestration, backend]
---
Single-agent workflows are reliable for bounded tasks. Multi-agent workflows — where
specialized agents hand off to each other through an orchestrator — are where the
coordination complexity lives, and getting the coordination wrong is worse than just
using a single agent for everything. I've built a few of these now and the patterns
that work are boring by design.

The orchestrator should be stateless and explicit. Each step receives a typed input,
produces a typed output, and records that result to durable storage before calling
the next step. The orchestrator reads from that storage; it doesn't rely on in-memory
state surviving across steps. This makes failure recovery trivial: on restart, the
orchestrator reads the storage, finds the last completed step, and continues. Without
it, a crash midway through a ten-step workflow means starting from zero, which is
expensive and sometimes incorrect if the first steps had side effects.

The individual agents should know nothing about the orchestration. An agent that takes
a typed input and returns a typed output can be tested in isolation, swapped for a
different implementation, or run manually for debugging. The orchestrator is the
policy; the agents are the mechanism. Keep that separation clean and the system
survives the inevitable requirement changes better than if every agent knows about
its neighbors.
