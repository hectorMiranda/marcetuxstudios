---
layout: post
title: "pgvector versus Pinecone for a real workload"
date: 2024-02-02
author: marcetux
tags: [ai, rag, postgres, embeddings, infrastructure]
---
A consulting client asked me to pick a vector store for their document retrieval
system and I told them to try pgvector first. They pushed back — they'd read that
dedicated vector databases scale better — and I pushed back harder, because their
workload was a few hundred thousand document chunks and one PostgreSQL instance they
already knew how to operate. Dedicated vector DB for that is buying a helicopter
when you need to cross the street.

pgvector adds two things to Postgres: a vector column type and an approximate
nearest-neighbor index (IVFFlat or HNSW). Query your embeddings with a SQL `ORDER BY
embedding <-> $1 LIMIT 20` and it works. You keep transactions, joins, row-level
security, your existing backup story, your existing monitoring. The performance at
sub-million row counts is fine. I ran benchmarks: HNSW index, 512-dim embeddings,
query latency under 20ms at p95. That is not a bottleneck for a retrieval-augmented
chat system.

Pinecone is the right answer when you've hit the limits of what Postgres can do —
hundred-million-scale, sub-10ms ANN with filtering across many index namespaces.
Most projects never get there. Starting with pgvector means you start with something
you already understand. Migrate when the numbers say migrate, not when the docs say
you might someday need to. The operational surface you don't add is the cost you
don't pay.
