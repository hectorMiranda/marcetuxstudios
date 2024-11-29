---
layout: post
title: "MCP follow-up after reading the spec more carefully"
date: 2024-11-28
author: marcetux
tags: [mcp, llm, ai, architecture, protocols]
---
I spent Thanksgiving week reading the MCP spec more carefully and building a small
toy implementation to understand the transport layer. The spec is cleaner than I
expected for something this new. JSON-RPC 2.0 as the transport means the framing is
already specified and understood; the MCP layer adds capability negotiation, a
resource type system (resources, tools, prompts), and a notification mechanism for
progress updates on long-running tools. The design decisions feel like they came from
people who had built LLM integrations before.

The thing I hadn't fully understood from the announcement: MCP distinguishes between
*resources* (data the model can read) and *tools* (actions the model can invoke).
This is a meaningful separation because the authorization story is different. A
resource reader needs read access; a tool invoker needs whatever access the tool
requires, which might include writes. The spec lets a server declare its capabilities
and a host negotiate down to what's safe to grant. That's a security design decision
embedded in the protocol, not left to the implementation.

The open question is the discovery layer. Right now, tool servers are configured
explicitly — you tell the host where to find the server. A registry or marketplace
where you browse available MCP servers would accelerate adoption significantly; that
piece isn't in the spec yet. I'll be building an MCP server for one of my consulting
tools in December to get real experience with the transport before it's no longer
the early-adopter phase.
