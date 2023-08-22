---
layout: post
title: "Streaming LLM responses and why it matters for UX"
date: 2023-08-21
author: marcetux
tags: [llm, ai, ux, architecture]
---
The difference between a streaming LLM response and a blocking one is about 20 seconds
on a long generation, and that 20 seconds is the difference between something that
feels like a tool and something that feels like a penalty. Most of the OpenAI API
tutorials show the blocking call — send prompt, wait, receive full response — because
it's simpler. Production interfaces stream.

The API supports server-sent events: the response arrives as a sequence of chunks,
each containing a delta of the generated tokens, terminated by `[DONE]`. The client
accumulates chunks and renders incrementally. On the server side, if you're building a
web API on top of the LLM API, you pipe the SSE stream through to the browser rather
than buffering the whole response and forwarding it. Each layer in the chain needs to
support streaming or the latency benefit collapses at that layer.

The implementation detail that bit me: HTTP clients and web frameworks have a buffering
default that breaks streaming unless you configure them otherwise. In Flask, you return
a `Response` with a generator and `mimetype='text/event-stream'` and you also have
to disable buffering in the WSGI server. In async frameworks like FastAPI with
Starlette, the `StreamingResponse` class exists for exactly this. The pattern is the
same everywhere; the configuration detail is different per framework, and the failure
mode — buffering until the response is complete — looks identical to a slow model.
