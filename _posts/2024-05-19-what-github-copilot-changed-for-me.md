---
layout: post
title: "What GitHub Copilot actually changed for me"
date: 2024-05-19
author: marcetux
tags: [ai, copilot, tooling, workflow, engineering]
---
I've been using Copilot since the technical preview and I want to give an honest
accounting of what it changed versus what it didn't. The "AI writes your code for
you" framing is wrong in a way that obscures what it's actually useful for. Copilot
doesn't write my code. It reduces the cost of a specific class of transitions: the
moment where I know what I want but have to type something I've typed a hundred
times before.

The wins are real and specific. Boilerplate that I know how to write but that slows
my thinking — test setup, serialization scaffolding, repetitive migrations — it
handles at typing speed. Regular expressions: I can write them, I hate writing
them, Copilot's first suggestion is usually right. Library calls I use rarely: I
know the shape but not the parameter names from memory; the suggestion shows me the
signature and I verify it's correct. These are time saves in the five-to-fifteen-
minutes range that compound into a noticeably faster morning.

What it didn't change: design decisions, architecture, debugging logic, reading
someone else's code, writing tests for novel behavior. The hard parts of engineering
are still hard. What changed is the glue between the hard parts — the scaffolding
you put up so you can get back to the hard part faster. I've become more productive
in the same way better autocomplete made me more productive, not in the way a better
architect would. That distinction matters when evaluating what to expect.
