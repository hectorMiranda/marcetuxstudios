---
layout: post
title: "Pi 5 as an edge inference node in practice"
date: 2024-07-13
author: marcetux
tags: [homelab, raspberry-pi, llm, edge-ai, hardware]
---
I've been running the Pi 5 as a local inference node for six months now and I have
enough hours on it to give a real accounting. The headline: for the workloads I
described in January — structured extraction, short-context QA, format conversion
— Mistral 7B at Q4_K_M quantization via Ollama is fast enough to feel interactive.
Seven to nine tokens per second on a cold context. Tolerable, not fast.

The thermal story is the honest part. The Pi 5 will throttle under sustained
inference load without active cooling. I added the official active cooler and the
throttling went away; sustained loads now stay at full 2.4 GHz. Without it, long
inference sessions — anything over a few minutes of continuous generation — would
pull the clock down and latency would double. This is not a surprise if you've built
anything with Pi hardware before, but it's the thing people don't mention when they
say "just run Ollama on a Pi."

The home lab use case I settled on: the Pi handles all the short-context, no-network-
required tasks. The queries it can't answer confidently — longer reasoning chains,
code review, anything where quality matters enough to wait — go to the cloud API.
The routing logic is the same dispatcher I described in March, now with three months
of production data. Local handles about two-thirds of the volume. The cost-per-task
on the third that goes to the cloud is low because it's genuinely complex. The math
works.
