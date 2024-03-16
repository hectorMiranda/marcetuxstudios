---
layout: post
title: "Routing between local and cloud models on the home lab"
date: 2024-03-16
author: marcetux
tags: [llm, ollama, homelab, ai, architecture]
---
I've been running Ollama on the Pi 5 since January and the honest accounting is that
the 7B quantized models are good enough for maybe 60% of what I send to a language
model: summarization, format conversion, code explanation, first-draft outlines.
The other 40% — nuanced reasoning, complex code review, anything where I need the
output to be actually right — still goes to a cloud API. The question is how to make
that routing not manual.

The setup I landed on: a small Python dispatcher that classifies the request by
rough complexity before sending it. Short input, deterministic expected output,
formatting or extraction task → Ollama. Long input, open-ended reasoning, anything
the classification is unsure about → cloud. The classifier is itself a lightweight
local model doing binary classification, which is almost circular but works well
enough. I log every decision and the routing outcome so I can tune the threshold
over time.

It's not a production system — it's a home-lab experiment to understand the trade-
offs. What it's teaching me is that "local vs. cloud" is not a model-quality
decision as much as a task-characterization decision. The mental work is defining
task types precisely, not picking the right model for every query. Push that upstream
and the model choice becomes mostly mechanical. The routing layer is the interesting
engineering.
