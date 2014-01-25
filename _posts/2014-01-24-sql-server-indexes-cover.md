---
layout: post
title: "Covering indexes and what they actually cover"
date: 2014-01-24
author: marcetux
tags: [sqlserver, sql, databases, performance]
---
Last year's execution-plan work paid off again this month. A query that summarized
customer bandwidth by region was fast on small data and slower with every month of
history added. The plan showed a key lookup after an index seek — the index was helping
the seek, but then SQL Server had to reach back to the clustered index to fetch columns
the index didn't include. That round-trip, multiplied by thousands of rows, was the
problem.

A covering index includes the extra columns in its `INCLUDE` clause. The nonclustered
index on `(customer_id, sample_date)` becomes `(customer_id, sample_date) INCLUDE (region,
bytes_in, bytes_out)`, and now the engine can satisfy the query entirely from the index
without touching the base table. The key lookup disappears from the plan, replaced by an
index-only scan. The query went from a couple of seconds to milliseconds.

The trade-off is index size and write overhead. Every `INCLUDE` column costs space and
makes every insert or update to those columns touch the index. So the guideline I use is:
add the column to `INCLUDE` only if the query hitting it is frequent enough and heavy
enough to justify the maintenance cost. The execution plan shows you the key lookup; it
doesn't tell you the cost of fixing it. That part still requires judgment.
