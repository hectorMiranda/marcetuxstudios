---
layout: post
title: "RAG is a retrieval problem first"
date: 2024-01-06
author: marcetux
tags: [ai, rag, embeddings, llm, architecture]
---
I spent a good chunk of December debugging a RAG pipeline whose answers were
confidently wrong, and the temptation was to blame the model. The model was fine.
The retrieval was the problem, and once I stopped staring at prompts and started
staring at what the retriever was actually returning, the fix was obvious.

The core issue was chunking. I'd split documents at fixed character counts and the
embedding index had chunks that cut mid-sentence, mid-table, mid-argument. The
similarity search would find the *words* nearby but return context that was
semantically half-baked. Switching to paragraph-boundary splits — plus an overlap
window so context doesn't vanish at the seam — changed the retrieved chunks from
"contains the keywords" to "contains the meaning." The model answered from those
chunks almost without fuss.

The second fix was adding a reranker pass between retrieval and generation. The
vector search is good at recall, not precision. A cross-encoder reranker takes the
top-k candidates, re-scores each one against the full query, and hands the generator
a smaller, tighter set. It adds latency but the quality delta is not subtle. RAG
improvements live in the retrieval step more often than the prompt. Chase the
source before you chase the phrasing.
