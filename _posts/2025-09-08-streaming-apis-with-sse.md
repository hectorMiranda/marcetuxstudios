---
layout: post
title: "Streaming API responses with Server-Sent Events"
date: 2025-09-08
author: marcetux
tags: [api, sse, streaming, backend, frontend]
---
LLM-powered features that stream their output need a different API pattern than
request-response. The client sends a request and receives a stream of tokens rather than
waiting for the complete response. The two options are WebSockets and Server-Sent Events,
and for this use case SSE is almost always the right choice: it's simpler, it runs over
standard HTTP so it works through load balancers and proxies that WebSocket upgrades
sometimes don't, and it's inherently one-directional, which is exactly what token
streaming is.

The .NET implementation with ASP.NET Core minimal APIs: return `IAsyncEnumerable<string>`
from the handler and let the framework negotiate the SSE framing. The more explicit
version is writing directly to `HttpContext.Response` with the correct `Content-Type:
text/event-stream` header and formatting each chunk as a `data: ...\n\n` event. The
explicit version gives you more control over retry behavior and event IDs, which matter
if the client might need to reconnect and resume a stream. For simple token streaming
the `IAsyncEnumerable` path is clean enough.

The client side in JavaScript uses the native `EventSource` API for simple cases; for
streaming over POST (which `EventSource` doesn't support) the Fetch API with a
readable stream on the response body is the standard pattern. The fetch-with-stream
approach has no IE compatibility concerns in 2025, which means you can use it without
apology or polyfill.
