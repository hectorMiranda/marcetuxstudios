---
layout: post
title: "Fine-tuning vs RAG, how to choose"
date: 2023-08-03
author: marcetux
tags: [llm, rag, fine-tuning, ai, architecture]
---
The question I keep getting asked in Slack and on calls is whether to fine-tune a
model or use RAG. Both approaches get you a model that responds well to domain-specific
questions, but they're doing completely different things and the wrong choice creates
problems that are expensive to undo.

Fine-tuning bakes knowledge into the model weights. You train on examples of the
behavior you want — question/answer pairs, correct output formats, domain vocabulary —
and the resulting model embeds that into its parameters. The advantage: no retrieval
step, no vector store, no context window management. The model just knows. The
disadvantage: knowledge becomes stale the moment the training set does, retraining
is expensive, and fine-tuning on factual recall tends to produce confident hallucinations
when the model is asked about something near the edge of its training.

RAG keeps the knowledge outside the model, in a store you control. Updates are as fast
as updating a document. You can inspect and audit what was retrieved. You can cite
sources because you know exactly what context was provided. The disadvantage: a query
that retrieves the wrong chunk, or retrieves nothing, gets a response based on training
weights alone, and the retrieval failure is invisible to the caller unless you design
for it. My rule of thumb: fine-tune for style, format, and persona; use RAG for facts.
A model fine-tuned to respond in a specific structure, using RAG to ground its answers,
is usually the right combination.
