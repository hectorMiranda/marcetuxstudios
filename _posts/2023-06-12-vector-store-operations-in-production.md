---
layout: post
title: "Vector store operations, the boring parts"
date: 2023-06-12
author: marcetux
tags: [rag, pgvector, databases, operations]
---
Three months of running a pgvector-backed RAG service for experiments has taught me
the operational things the tutorials skip. The tutorials show you how to embed and
query; the operations are about what happens when your corpus changes, when the query
plan degrades, and when the index doesn't match the data.

The first gotcha: IVFFlat indexes go stale. The index partitions (the "lists") are
built from a snapshot of the data distribution at index creation time. If you insert
a lot of new documents without rebuilding the index, the new vectors end up in
clusters that don't reflect where they live in the embedding space, and recall
degrades. The fix is to `REINDEX` periodically — I do it after every bulk import.
`VACUUM ANALYZE` first so the planner has current stats.

The second: monitoring semantic drift. If you change embedding models — different
version, different provider — vectors from the old model and the new model are not
comparable. They live in different spaces. A mixed-model index gives wrong results
silently; there's no error, just quietly worse retrieval. Tag every vector row with the
model ID that produced it and refuse to mix them in a query. The `model_id` column
is boring infrastructure that saves you a very confusing debugging session.
