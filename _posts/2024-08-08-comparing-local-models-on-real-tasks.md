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

*Update: I also ran these models against a second task — code explanation for the
tutoring work — and the ranking shifts. Phi-3 Mini degrades on code that mixes
multiple languages or frameworks; Llama 3 8B stays solid. Gemma 2 produces longer
explanations than students typically find useful without additional instructions to
keep answers brief. Mistral 7B is the surprise winner for short code explanation:
concise by default, correct on common patterns. The right model varies by task more
than I had accounted for. Worth measuring your actual task, not just one.*
