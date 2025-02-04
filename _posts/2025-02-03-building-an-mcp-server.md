---
layout: post
title: "Building an MCP server from scratch"
date: 2025-02-03
author: marcetux
tags: [mcp, ai, agents, tooling, python]
---
After the January survey of the MCP spec I picked a real use case and built an actual
server: wrapping the internal project-tracking API a client uses so their agentic
workflow could query and update tickets without me writing custom glue for every agent
host they try. Three hours of work, most of it understanding the existing REST API.

The implementation is simple enough that I'd call it boring, which is the right sign.
You declare your tools as JSON schema, implement a handler for each, and serve the whole
thing over stdio or HTTP-SSE. The Python SDK handles the protocol framing. The only
real decision was where to put authorization — I settled on passing a token in the
server config at startup rather than threading it through every tool call, which keeps
the tool schemas clean. The agent gets "create ticket" and "search tickets"; it doesn't
need to know anything about auth.

The test: swap from the agent host we'd been using to a different one, point at the
same MCP server. Tools showed up correctly on the first try, no changes to the server.
That's the composability argument proved in miniature. I'm converting more internal
utilities to MCP servers as I go — it's a cleaner abstraction than a pile of bespoke
function-calling schemas.
