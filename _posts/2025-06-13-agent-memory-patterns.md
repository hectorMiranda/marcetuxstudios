---
layout: post
title: "Memory patterns for persistent agents"
date: 2025-06-13
author: marcetux
tags: [agents, ai, architecture, memory, llm]
---
An agent that forgets everything between sessions is useful for bounded tasks. An agent
that's supposed to be an ongoing assistant — the kind clients are starting to build for
internal tooling — needs some form of persistent memory, and "just put everything in the
context" is not the right answer past a few sessions. This month I've been working
through what the options actually look like in practice.

The pattern I've found most reliable is tiered memory: a working context with recent
interaction history (last few exchanges, current task state), a semantic store with
durable facts indexed by embedding (things the agent learned that might be relevant to
future sessions), and a structured store with precise records (user preferences, project
state, decisions made). Each tier has a different retrieval mechanism: the working
context is passed directly, the semantic store is retrieved by similarity, the structured
store is queried by key. The tiers don't compete; they complement.

The discipline is writing to the right tier. An instruction the user gave once and will
give again ("always format responses as bullet points") belongs in the structured store,
not in every context window. A piece of domain knowledge the agent picked up during a
research task belongs in the semantic store. Recent conversation history belongs in
working context and gets summarized or dropped after the session. The boundary decisions
are the hard part; the implementation of each tier is not.
