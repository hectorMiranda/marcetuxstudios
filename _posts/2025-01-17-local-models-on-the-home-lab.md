---
layout: post
title: "Running local models on the home lab in 2025"
date: 2025-01-17
author: marcetux
tags: [llm, local-models, homelab, hardware, ai]
---
The NAS and Pi cluster are still in the bedroom while I prep the Lincoln Heights space,
but I did get the workstation stood up there this week with a proper GPU. The first
thing I installed was Ollama, and the experience is genuinely different from the local
model experiments I was running a year ago — not because the software changed that much,
but because the models did. A 7B-parameter model running on a local GPU in early 2024
was a parlor trick. The same class of model in early 2025 is actually useful for
coding tasks, summarization, and first-pass review work.

What makes local models worth the hassle in a consulting context is the data boundary.
When a client has restrictions on sending their proprietary code or documents to an
external API, a local model is the difference between "we can use this" and "legal says
no." Inference speed is slower than a hosted API and the ceiling on capability is lower,
but "good enough and allowed" beats "great and blocked" every time.

The practical stack: Ollama for model management, an OpenAI-compatible local endpoint
that most tooling speaks without modification, and a small quantized model suited to
the task at hand. I've stopped trying to run one model for everything; the right answer
is a model the task fits into memory, not the biggest one that fits on the disk.
