---
layout: post
title: "MCP, the protocol that arrived quietly at the end of last year"
date: 2025-01-06
author: marcetux
tags: [mcp, ai, agents, tooling, protocols]
---
Anthropic shipped the Model Context Protocol spec in late November and I missed it in
the holiday noise. I only noticed when a client asked why their agent setup was
reinventing the same tool-calling glue for the third time. Reading through MCP made the
reinvention feel avoidable.

The idea is boring-good: standardize how a host application exposes tools, resources,
and prompts to a language model. Instead of every agent framework defining its own
schema for "call a function," "read a file," or "list available actions," MCP is a
single interface both sides agree on. The model sends a structured request; the server
executes it and returns a structured result. The transport is JSON-RPC over stdio or
HTTP-SSE, nothing exotic. What that buys in practice is composability — an MCP server
you write for one agent host can plug into another without any glue code, the same way
an HTTP service can talk to any HTTP client.

Adoption is early but I'm already seeing tools ship MCP servers as a first-class
artifact alongside their APIs. That trajectory looks familiar: it's how OpenAPI went
from nice-to-have to assumed. I'm experimenting with wrapping some client tooling as
MCP servers this month. Report to follow when I have something concrete.
