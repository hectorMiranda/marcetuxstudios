---
layout: post
title: "Security posture for agentic systems"
date: 2025-09-03
author: marcetux
tags: [security, agents, ai, architecture, platform]
---
The security review for an agent system looks different from a security review for a
REST API, and teams that treat it the same way are missing the specific risks. The
classic API security concerns — injection, broken auth, excessive data exposure — still
apply, but agents add a new category: the model itself can be manipulated to take
actions the system designer didn't intend, through prompt injection in the data the
agent processes.

Prompt injection through external data is the attack surface that most implementations
underestimate. If an agent reads documents, emails, or web pages as part of its task,
and those artifacts contain text that looks like instructions, the model may follow
those instructions rather than its system prompt. The defense is structural: treat
external data as untrusted user content at the model level, not as trusted context.
Some implementations accomplish this by putting external data in a separate context
position with explicit labeling; others verify agent outputs against expected schemas
before acting on them. Both help. Neither is complete.

The principle I've settled on for agentic system security: minimize capability, confirm
before irreversible action, log everything. An agent that can read widely and write
narrowly has a small blast radius. An agent that pauses and confirms before deleting
or sending gives the human a recovery window. Comprehensive logging means the incident
review has a trail. These are old principles applied to a new attack surface.
