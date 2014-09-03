---
layout: post
title: "Partial indexes in PostgreSQL for sparse data"
date: 2014-09-02
author: marcetux
tags: [postgresql, sql, databases, performance, indexes]
---
The email notification queue table at Spark has a `sent_at` column that's NULL for every
unsent row. A query that fetches unsent rows — `WHERE sent_at IS NULL` — was scanning
the whole table because a standard index on `sent_at` doesn't efficiently handle NULL
values, and the table has millions of rows where `sent_at` is not NULL. Most of the
table is irrelevant to the query, and the index was including it anyway.

A partial index is an index with a `WHERE` clause. `CREATE INDEX CONCURRENTLY idx_email_queue_unsent
ON email_queue (created_at) WHERE sent_at IS NULL` creates an index that contains only
the rows where `sent_at IS NULL` — the rows the query actually needs. The index is a
fraction of the full table size and gets a perfect index-only scan on the query. The
sent rows, which dominate the table, aren't in the index at all.

`CONCURRENTLY` is the keyword that makes this safe to run in production. Without it,
the index build holds an exclusive lock on the table for the duration — minutes on a
large table, unacceptable. With `CONCURRENTLY`, the build runs while normal reads and
writes proceed, at the cost of taking slightly longer. Building indexes on live tables
without `CONCURRENTLY` is an outage.
