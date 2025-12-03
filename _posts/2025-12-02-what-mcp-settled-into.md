---
layout: post
title: "What MCP settled into by the end of 2025"
date: 2025-12-02
author: marcetux
tags: [mcp, ai, agents, tooling, ecosystem]
---
A year ago MCP was a spec Anthropic had published and a handful of early implementations.
Today it's the default way to expose tools to agents in every serious toolchain I work
with. The adoption curve was faster than I expected — not because the spec is perfect,
but because the problem it solves is universal enough that everyone felt the pain at
roughly the same time.

The pattern that emerged: teams build MCP servers for their internal systems — their
project trackers, their data warehouses, their internal APIs — and use agent hosts that
speak MCP natively. The server is a clean abstraction boundary; the agent doesn't know
or care what's on the other side. This is the boring-good outcome I described in
January, and it held up. The composability argument proved out in practice, not just in
theory.

The quality variance I flagged in April is still real. There's a lot of MCP server code
in the world now that was written quickly and doesn't handle error cases cleanly. The
pattern of agents confidently using a broken tool is more common than it should be. The
discipline of evaluating your tools, not just your model, is still under-practiced. The
ecosystem matured in adoption faster than it matured in quality.
