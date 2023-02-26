---
layout: post
title: "pgvector and embeddings in Postgres"
date: 2023-02-06
author: marcetux
tags: [postgres, embeddings, ai, llm]
---
The thing that changed my weekend experiment this month was a Postgres extension called
`pgvector`. The premise is simple: store vectors as a column type, build an approximate
nearest-neighbor index on them, and do semantic search inside the database you already
have. I'd been reading about vector databases as a new category, and then I realized
I could try the concept against a Postgres instance I already own.

The workflow: feed text through an embedding model to get a fixed-length float vector —
I used OpenAI's `text-embedding-ada-002` — store it alongside the original row, and
then query with the `<=>` operator for cosine distance. Results are the rows whose
text is semantically closest to a query string, not lexically. I indexed about four
thousand blog-post-sized documents and queries came back under 50ms. The index type
for approximate search is IVFFlat: cluster the vectors first, then search the nearest
cluster. Exact nearest-neighbor scales badly; approximate is the practical choice.

The reason I'm excited: this isn't a new database to operate or a new infrastructure
tier to justify. If you already have Postgres, `pgvector` is a `CREATE EXTENSION` and
a column type. The abstraction cost of a purpose-built vector DB is worth it at some
scale or feature requirement, but that scale is not where most projects start.

*Update: after indexing a larger corpus (around 100k rows), the IVFFlat `lists` setting matters more than I implied. The rule of thumb is `sqrt(rows)` for up to a million rows and `rows / 1000` after that. Also worth noting: the index must be built after rows are inserted — an empty-table index won't rebalance. Added a `VACUUM ANALYZE` pass before the index creation in my actual scripts.*
