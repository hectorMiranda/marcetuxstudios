---
layout: post
title: "Building a small MCP server from scratch"
date: 2024-12-02
author: marcetux
tags: [mcp, llm, ai, python, architecture]
---
I built an MCP server over the weekend that exposes a small knowledge base of
consulting notes as resources. The implementation language is Python; the transport
is stdio, which the spec supports and which is the simplest option for a server that
runs locally. Two weekends in, I have something that works end to end: the server
lists available resources, returns content when asked, and a host that speaks MCP
can browse and retrieve the notes through the protocol.

The implementation surface is smaller than I expected. The JSON-RPC framing is
boilerplate — read a line, parse it, dispatch by method, write a response line. The
MCP-specific methods are: `initialize` (negotiate capabilities), `resources/list`
(return the catalog), `resources/read` (return one resource's content), and for tools:
`tools/list` and `tools/call`. A minimal server that supports resources only is
maybe three hundred lines of Python without shortcuts. With a library doing the
JSON-RPC layer it's under a hundred.

The part that clarified the design for me: resources have a URI scheme and the server
controls the scheme. My consulting notes live at `notes://2024/<topic>`. A client
that doesn't know what `notes://` means can still list and read them because the
protocol layer handles the transport; the URI is just an identifier. When the host
passes the URI back to the server for reading, the server maps it to the actual file.
The client never needs to know what's behind the URI. That's the right abstraction:
decoupled from the storage layer, typed at the resource level.
