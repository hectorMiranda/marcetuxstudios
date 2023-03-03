---
layout: post
title: "RAG, the pattern behind the hype"
date: 2023-03-02
author: marcetux
tags: [llm, rag, ai, architecture]
---
Retrieval-Augmented Generation has a terrible name and a surprisingly simple idea:
instead of trusting what a language model memorized during training, you retrieve
relevant text at query time and put it directly in the prompt. The model's job shifts
from "remember this fact" to "read this context and answer the question." That
distinction is the whole thing.

The pattern crystallized for me through the pgvector experiments from February. You
embed the user's question, find the nearest document chunks in the vector store, stuff
them into the prompt as context, and then ask the model to answer only from what's
provided. The model still handles the language — summarizing, reasoning, formatting
— but the facts come from your retrieval step, which you control. That means you can
update the knowledge base without retraining the model, and you can cite sources
because you know exactly what was retrieved.

The failure modes are worth naming: retrieval can miss the relevant chunk (embedding
similarity isn't semantic understanding), the context window fills before all relevant
chunks fit, and a model can still confabulate even with good context if the question is
ambiguous. But compared to a bare model hallucinating from training weights, RAG shifts
the problem to one you can actually debug. Wrong answer? Check what was retrieved. Fix
the retrieval. That's an engineering problem I know how to approach.
