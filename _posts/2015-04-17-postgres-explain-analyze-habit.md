---
layout: post
title: "Making EXPLAIN ANALYZE a habit"
date: 2015-04-17
author: marcetux
tags: [postgresql, databases, performance, sql]
---
The member-search query we added in Q1 started drifting slow as the dataset grew past
a million profiles. My first instinct was indexes; the actual fix was a query rewrite
that happened because I ran `EXPLAIN ANALYZE` before touching the schema. Same lesson
as February's post, different table. Apparently I need to re-earn this habit.

`EXPLAIN ANALYZE` runs the query and shows the actual execution times alongside the
estimates, which is the key detail — `EXPLAIN` alone shows what Postgres *plans* to do;
`ANALYZE` shows what it actually did, including row counts. When the estimated rows are
off by an order of magnitude, the planner's choice of join strategy or index is probably
wrong for the actual data. In our case, a very selective filter on a rarely-used column
looked cheap to the planner but was hitting a sequential scan because the statistics
were stale — `ANALYZE` on the table refreshed the statistics and the planner immediately
chose the index.

The workflow I'm trying to lock in: before writing an index migration, run
`EXPLAIN ANALYZE` on the slow query, screenshot the output, then reason about what it's
telling you. Most of the time the fix is cheaper than a new index, and the times when
you do need an index you know exactly what kind and why. The plan is the database
telling you; stopping to listen is the discipline.
