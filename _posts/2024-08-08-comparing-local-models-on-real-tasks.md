---
layout: post
title: "Comparing local models on real extraction tasks"
date: 2024-08-08
author: arcetux
tags: [llm, ollama, ai, evaluation, models]
---
I spent a week running my extraction golden dataset — the invoice-extraction eval from
January — across four models available in Ollama: Llama 3 8B, Mistral 7B, Phi-3
Mini, and Gemma 2 9B. All at Q4_K_M quantization, all on the same Pi 5 hardware,
same prompts. This is not a rigorous benchmark; it's the kind of comparison that
tells you which model to use for your specific task on your specific hardware.

Extraction accuracy on structured fields: Gemma 2 9B led, followed closely by
Llama 3 8B. Phi-3 Mini was a surprise — despite being significantly smaller, it was
within a few points on simple field extraction and appreciably faster. Mistral 7B was
mid-pack. On the tasks with ambiguous field values — vendor names formatted
differently across invoices, date formats mixing MM/DD and DD/MM — Llama 3 pulled
ahead clearly. The Phi-3 Mini gap widened when ambiguity required inference.

The practical outcome: Phi-3 Mini for high-volume, well-structured documents where
latency matters. Llama 3 8B for anything with ambiguity or irregular formatting.
Gemma 2 for tasks where accuracy ceiling matters most and you can wait for throughput.
The right model for a task is an empirical question, not a chart question. Running
your own task against your own data for ten minutes tells you more than a benchmark
paper. I should have done this comparison six months ago instead of defaulting to
Mistral out of habit.
