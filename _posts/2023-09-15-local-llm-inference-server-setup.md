---
layout: post
title: "Setting up a local LLM inference server"
date: 2023-09-15
author: marcetux
tags: [llm, home-lab, inference, llama, ai]
---
The setup I've been running all year — `llama.cpp` invoked from a shell script —
works for batch jobs but is awkward for anything that needs an API. There's now a
better option: `llama.cpp` gained a `--server` mode that exposes an HTTP endpoint
compatible with the OpenAI API format. Point the same client code that talks to
OpenAI at `localhost:8080`, and it hits the local model instead. No code change.

The architecture on the home x86 machine: `llama.cpp` in server mode with the Llama
2 13B 4-bit model, fronted by a simple nginx reverse proxy that handles basic auth.
Memory usage is about 8GB resident for the 13B at q4 quantization. I set
`--parallel 1` — one request at a time — because the machine has no GPU and parallel
requests would just serialize anyway. The server stays warm between requests so
the model-load time doesn't repeat.

The OpenAI API compatibility is useful enough to be the whole point. LangChain,
LlamaIndex, and any other library built on top of the OpenAI SDK can be redirected
to the local server with a one-line change to the base URL. The local model doesn't
match GPT-3.5's quality, but for tasks that run overnight, for development testing,
or for anything that shouldn't leave the local network, having an API-compatible
local endpoint is a real upgrade from "run this script and wait."
