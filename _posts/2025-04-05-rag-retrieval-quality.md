---
layout: post
title: "RAG retrieval quality is the bottleneck, not the generation"
date: 2025-04-05
author: marcetux
tags: [rag, llm, ai, retrieval, architecture]
---
I've audited four RAG implementations in the last two months and every one of them had
the same shape: a well-configured embedding model, a vector store with sensible
settings, and a generation step that was perfectly adequate — and retrieval that was
quietly returning the wrong chunks. The teams had spent most of their tuning effort on
the generation side. The retrievals were off and nobody had looked.

Retrieval quality is boring to measure and central to results. The metric I start with
is recall-at-k: for a set of questions where you know the ground-truth document, how
often does the retrieved top-k include it? If recall at k=5 is 60%, meaning four out of
ten questions don't even retrieve the relevant document, no amount of prompt engineering
will save the answer quality. Fix the retrieval first. Common culprits: chunks that are
too small to carry useful context, chunks that cross natural document boundaries,
embeddings trained on a domain too far from your corpus.

The fix that moves the needle most often is chunk strategy. Semantic chunking — breaking
documents at meaningful boundaries rather than fixed character counts — improves recall
without changing anything about the vector store or the model. It's a text preprocessing
concern, unglamorous, and it matters more than the embedding model choice in most
practical cases.
