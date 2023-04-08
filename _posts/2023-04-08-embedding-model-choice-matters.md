---
layout: post
title: "Embedding model choice matters more than I thought"
date: 2023-04-08
author: marcetux
tags: [llm, embeddings, rag, ai]
---
I spent a weekend benchmarking three embedding models against the same corpus —
OpenAI's `text-embedding-ada-002`, and two local models from the
`sentence-transformers` library (`all-MiniLM-L6-v2` and `all-mpnet-base-v2`) — and
the retrieval quality difference is larger than I expected for a component people
treat as interchangeable.

The test: 200 hand-written query/expected-document pairs from my personal notes corpus.
Ada-002 returned the correct document in the top 3 results for 87% of queries. MPNet
hit 79%. MiniLM hit 71%. The gap between MiniLM and Ada-002 is 16 percentage points
on this corpus, which is not noise — it's "retrieval that often works" versus "retrieval
that usually works." The larger sentence-transformers models close the gap but at the
cost of running slower on the Pi and eating more RAM.

The decision tree I've landed on: prototype with MiniLM because it's fast and free
to run locally; validate with MPNet when you care about quality; pay for Ada-002 when
the accuracy gap justifies it or when you're already paying for GPT calls anyway. The
chunking strategy interacts with this too — a better model won't save a bad chunking
scheme. But if you've got the chunking right and retrieval is still missing, the model
is the next place to look.
