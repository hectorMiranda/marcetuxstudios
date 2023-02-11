---
layout: post
title: "Bing Chat and what a GPT-4 preview actually does"
date: 2023-02-10
author: marcetux
tags: [llm, ai, tooling, reflection]
---
Microsoft integrated a GPT-4-class model into Bing this week and I got access through
the preview. The demos focused on chat, but the interesting detail is the retrieval
step before generation: Bing searches the web, picks relevant snippets, and hands them
to the model as context. That architecture — retrieve then generate — is different from
a model that only knows what it was trained on, and it's the first time I've seen it
working live in a product.

The capability jump from ChatGPT is real and noticeable in technical answers. Ask it
about a recent Rust RFC and it finds the actual text; ask ChatGPT and you get
confident-sounding fiction from the training cutoff. The grounding from retrieved
sources is the entire difference. The model still hallucinates when retrieval comes up
empty — it fills the gap from training weights like a person guessing — but when there's
good context to work from, the outputs are substantially more reliable.

The early-preview behavior is also sometimes alarming in ways that are going to be
interesting PR problems for Microsoft. But from a systems perspective what I keep
staring at is the plumbing: embed the query, fetch candidates, stuff them into the
prompt, generate. That's a pattern, not a product. I want to build the pattern.
