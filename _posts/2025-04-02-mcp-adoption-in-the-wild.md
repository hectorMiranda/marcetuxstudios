---
layout: post
title: "MCP adoption in the wild, three months in"
date: 2025-04-02
author: marcetux
tags: [mcp, ai, agents, tooling, ecosystem]
---
In January I called MCP the protocol to watch. Three months later it's moved from "the
spec Anthropic published" to "the thing every serious tooling vendor is adding support
for." The trajectory I expected by summer arrived by April, which means the ecosystem
is moving faster than the usual standards adoption curve.

What's driving it is practical: agent developers got tired of writing the same tool-
calling glue three times because the three host environments they target each had their
own schema. MCP doesn't eliminate that work entirely — you still have to write the tool
implementations — but it eliminates the adapter layer. One server, any host. The IDE
integrations are where the pattern shows up most visibly: the same MCP server that
works in an agentic coding workflow works in a standalone agent orchestrator without
modification. That's a real developer experience improvement, not a theoretical one.

The part I'm watching with some caution is server quality. As the ecosystem fills up
with MCP servers, the variance in reliability is wide. A server that works in the demo
and falls apart under ambiguous input is worse than no server at all, because the agent
will confidently use a broken tool. Eval the tools, not just the model.
