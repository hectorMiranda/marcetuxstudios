---
layout: post
title: "Edge AI and what actually runs on small hardware"
date: 2024-04-08
author: marcetux
tags: [edge-ai, hardware, llm, embedded, homelab]
---
I've been experimenting with inference on the smallest hardware I have — an ESP32-S3
and the Pi 5 — and the constraints are clarifying in a way that running cloud APIs
isn't. On the ESP32 the practical upper limit for a useful model is a few hundred
kilobytes after quantization. That rules out anything language-model-shaped; what
fits is small classifiers, keyword spotters, and lightweight anomaly detectors. The
value isn't "LLM at the edge" — it's "inference that never leaves the device."

On the Pi 5 with 8 GB, the picture is better. GGUF 4-bit quantized 7B models run
comfortably. I've been using Phi-2 and Mistral 7B for local extraction tasks and
they're fast enough to feel interactive. The bottleneck isn't compute — it's I/O:
the context window fills, and streaming the output tokenizes slowly over a long
context. For short-context classification and extraction the throughput is genuinely
useful. For long-document work the latency is where you set it and wait.

The shape of an edge AI system is different from a cloud AI system. Cloud: one big
capable model for everything, low-latency network, API budget matters. Edge: many
small specialized models, no network round-trip, power and memory budget matter
instead. I don't think it's cloud versus edge; it's choosing where the intelligence
lives for each decision. The local classifier that pre-routes traffic before
anything hits the cloud API is a legitimate architecture. What's running on the
device shapes what needs to leave it.
