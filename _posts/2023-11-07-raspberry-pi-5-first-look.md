---
layout: post
title: "Raspberry Pi 5, first look from the home lab"
date: 2023-11-07
author: marcetux
tags: [raspberry-pi, electronics, home-lab, llm]
---
The Pi 5 arrived last week. The headline specs are a 2.4GHz Arm Cortex-A76 — roughly
double the throughput of the A72 in the Pi 4 — and a new RP1 I/O controller that moves
USB, Ethernet, and the PCIe lane off the SoC. There's an actual PCIe x1 connector on
the board, which means M.2 HATs and real NVMe storage are coming. That changes the
home-server conversation.

The immediate test I care about: local LLM inference. Running the Llama 2 7B at 4-bit
quantization with `llama.cpp` on the Pi 4 gave me about 2 tokens/second. On the Pi 5
at the same settings it comes back at roughly 4.5 tokens/second. Not fast, but double
is double. For a machine that costs $80 and sits on a shelf consuming under 10 watts,
that's a meaningful improvement. The Pi 5 can host an inference server for overnight
batch jobs — summaries, embeddings generation, offline RAG queries — without competing
with the x86 box for tasks.

The thermal situation is worth noting. The Pi 5 runs hot under sustained load — it
needs active cooling, not just the passive heatsink. The official active cooler is the
right answer and it's audible but not obnoxious. Running `llama.cpp` inference for
20 minutes without cooling, the CPU throttles to protect itself and inference speed
drops. Active cooler on from the start; don't learn that the slow way.
