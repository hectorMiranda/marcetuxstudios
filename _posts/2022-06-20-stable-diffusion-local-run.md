---
layout: post
title: "Running Stable Diffusion locally and what it changes"
date: 2022-06-20
author: marcetux
tags: [ai, stable-diffusion, machine-learning, homelab]
---
Stable Diffusion shipped in late August — I'm writing this a couple of weeks after
getting my hands on the weights and running it locally on my home machine, and the
experience is different from DALL-E 2 in a way that matters. DALL-E 2 is an API: you
send a prompt, you pay per image, OpenAI's servers do the work. Stable Diffusion is a
model you download and run yourself, on your GPU, for free, with no content moderation
and no rate limit.

The hardware requirement is a GPU with at least 4–6GB of VRAM for the standard model.
My machine has an RTX 3070 with 8GB and it generates a 512x512 image in about eight
seconds with 50 diffusion steps. The diffusion process is what makes these models
interesting mechanically: you start from pure noise and iteratively subtract predicted
noise until you've converged on a coherent image conditioned on the text prompt. Fewer
steps means less quality; more steps has diminishing returns past around 50. The output
quality from the open-weights model is behind DALL-E 2's best outputs, but the gap is
smaller than I expected and closing.

What actually changes with local weights is the experimentation loop. I ran thirty
variations on a PCB closeup prompt in twenty minutes, which would have cost money and
time against an API. The ability to iterate freely on prompts, samplers, and seed
values without thinking about cost produces a different relationship with the tool.
I'm using it now for concept art for the next PCB design.
