---
layout: post
title: "Mistral 7B, a small model worth running locally"
date: 2023-12-04
author: marcetux
tags: [llm, open-source, mistral, home-lab, ai]
---
Mistral AI released a 7B model in late September and the benchmarks were good enough
to make me suspicious — a 7B model outperforming Llama 2 13B on several reasoning
benchmarks is either a measurement artifact or something genuinely interesting in the
architecture. After running it for a few weeks I'm satisfied it's the latter.

The architectural difference is Grouped Query Attention and Sliding Window Attention.
GQA reduces the memory bandwidth required for the key-value cache during inference,
which is why it runs faster at a given parameter count than a naive attention
implementation. Sliding Window Attention limits each token's attention to a fixed
window of recent tokens rather than all previous tokens, which reduces the quadratic
complexity of attention at long contexts. The practical effect: Mistral 7B runs at
about 6 tokens/second on my x86 box with `llama.cpp` compared to about 8 for Llama
2 7B, but the output quality on code generation and reasoning tasks is noticeably better.

For local inference on hardware without a GPU, Mistral 7B is now my default. The Pi 5
runs it at about 3.5 tokens/second — slower than Llama 2 7B on the same hardware,
but better quality per token is the right trade when I'm not watching the output in
real time. Overnight batch jobs care about quality. Interactive sessions care about
speed. Know which you're building.
