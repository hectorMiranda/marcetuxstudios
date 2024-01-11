---
layout: post
title: "Running local models with Ollama"
date: 2024-01-10
author: marcetux
tags: [llm, ollama, homelab, hardware, ai]
---
Ollama landed on my Pi 5 over the holiday break and it's the friendliest I've ever
found this kind of setup. The pitch is simple: pull a model, run it locally, use the
same API shape as OpenAI so anything you've already built can swap endpoints. No
cloud account, no per-token bill, no data leaving the house.

What surprised me is how capable the smaller quantized models are for constrained
tasks. Llama 2 7B at 4-bit quantization fits in the Pi's 8 GB and can answer
structured-extraction queries reliably when the context is short and the instruction
is precise. It's not GPT-4 — hallucinations on long chains are more frequent and
it struggles with nuance — but for "extract these fields from this document" it is
genuinely good enough and entirely offline. I've been routing tutoring-prep tasks
through it and the throughput is tolerable once I stop expecting cloud response times.

The more interesting thing is what the setup teaches you about the model's actual
requirements. You can't hide behind "just send it to the API" anymore — you have to
think about context length, quantization trade-offs, memory pressure. I'd rather
understand those constraints by living with them than discover them when I'm
integrating a vendor model into something a client depends on.
