---
layout: post
title: "Pinecone vs pgvector for a small project"
date: 2023-03-22
author: marcetux
tags: [llm, rag, pgvector, databases, architecture]
---
The comparison I keep seeing in every RAG tutorial is "use Pinecone" — Pinecone being
the managed vector database that's been getting a lot of marketing dollars alongside
the LLM wave. I spent an afternoon moving the same document corpus from my local
pgvector setup to Pinecone's free tier, and I want to be precise about the trade-off.

Pinecone is genuinely easier to start with if you have no database to run: you create
an index in the console, upload vectors via their SDK, and query. There's no
infrastructure. For a prototype that exists only in a notebook, that's a real
advantage. The query latency on the free tier was comparable to my local pgvector —
15–30ms at this data volume. The hosted durability story is also better than "my Pi
stays up."

But for any project that already has a relational database — which is nearly every
production system I've touched — pgvector keeps the vectors in the database where the
rest of the data lives. Joins, transactions, backups, access control: all the things
you already pay for operationally. Pinecone is a separate service to operate, a
separate failure domain, a separate bill, and you're syncing state between two stores.
At small-to-medium scale, "add a column and an extension" beats "add a service." At
large scale with demanding ANN requirements, revisit.
