---
layout: post
title: "Running Llama 2 locally, the actual setup"
date: 2023-07-10
author: marcetux
tags: [llm, llama, home-lab, ai, raspberry-pi]
---
The day after the announcement I was setting up Llama 2 on the home lab, and the
tutorial landscape is already crowded but most of it assumes a Mac with Apple Silicon
or an NVIDIA GPU. My situation is different: a Pi 4 (4GB) and an older x86 machine
with no GPU. So I went through the `llama.cpp` path, which is the community project
that runs LLM inference in pure C++ with optional SIMD and GPU offload.

The 7B model in 4-bit GGML format: about 3.8GB on disk. `llama.cpp` compiled from
source with the appropriate CPU flags — `LLAMA_AVX2=1` on the x86 box — and ran
inference at about 8 tokens/second on that machine. On the Pi 4 it's closer to 2
tokens/second at 4-bit quantization, which is slow but not unusable for
non-interactive tasks. For a script that summarizes text overnight, 2 tok/sec is fine.
For an interactive chat session, it's miserable.

The x86 box at 8 tok/sec is usable for async workflows but not for real-time chat
without a spinner and patience. The Apple Silicon benchmarks people are posting — 30+
tok/sec on M2 for the 13B — reflect hardware I don't own. The lesson: check the actual
hardware you have before committing to a local inference setup. The model is free;
the compute cost is still real, it's just in hardware you're buying rather than API
credits you're spending.
