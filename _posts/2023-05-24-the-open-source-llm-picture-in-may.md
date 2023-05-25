---
layout: post
title: "The open-source LLM picture in May"
date: 2023-05-24
author: marcetux
tags: [llm, open-source, ai, reflection]
---
The LLM space in May has the same chaotic energy I remember from the Docker-and-
microservices wave in 2014: new releases every week, dramatic capability claims,
a lot of "you need this" from people who built the thing. The difference is the
releases are mostly closed — GPT-4, PaLM 2, and the other proprietary frontier models — and the open-source
alternatives are catching up faster than the closed-source incumbents are comfortable
admitting.

Meta's LLaMA weights leaked in March. Models fine-tuned on those weights — Alpaca,
Vicuna, and others — run on consumer hardware and show that the parameter count
required for useful task performance is lower than the big-lab benchmarks implied.
The leaked weights created an interesting legal question, but the practical effect is
that a lot of researchers who couldn't afford API costs now have a capable base model
to work with. Open-source models won't match GPT-4 on the benchmarks this month.
They don't need to — they need to be good enough for specific tasks and cheap to run.

My interest is what happens at inference time when the model lives on hardware I
control. No rate limits, no data egress, no per-token billing. The productivity apps
and the creative experiments all optimize toward the API because the quality is better
there. The privacy-sensitive and latency-critical applications optimize toward local.
I'm watching the local side get better fast.
