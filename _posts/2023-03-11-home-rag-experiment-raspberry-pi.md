---
layout: post
title: "Home RAG experiment on Raspberry Pi"
date: 2023-03-11
author: marcetux
tags: [llm, rag, raspberry-pi, home-lab, electronics]
---
I have a Pi 4 running on the home network that I repurposed for the weekend: set it
up as a tiny RAG server over my personal notes — markdown files from the last few years
— using a local vector store and the OpenAI API for the embedding and generation
steps. The Pi does the indexing and retrieval; the expensive model calls go out to the
API. It's a deliberate split: do as much local as possible, pay for inference only.

The stack: `sentence-transformers` running locally for embeddings (specifically
`all-MiniLM-L6-v2`, which is small enough the Pi handles it in a few seconds per
chunk), ChromaDB as the on-disk vector store, and a small Flask endpoint that takes
a query, retrieves the top-5 chunks, and fires a GPT-3.5-turbo completion with those
chunks as context. Total memory footprint under 800 MB, runs fine on the Pi 4's 4GB.
A query round-trip — local retrieval plus API call — is about two seconds.

The retrieval quality over personal notes is better than I expected and worse than I
hoped for vague questions. Specific queries ("what did I write about idempotency
in 2020?") work well; diffuse ones ("what's my take on microservices?") retrieve
random snippets that don't synthesize cleanly. That's the retrieval problem, not the
generation problem. Next step: better chunking — split at logical boundaries, not
character count.
