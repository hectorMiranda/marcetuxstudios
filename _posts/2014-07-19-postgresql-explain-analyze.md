---
layout: post
title: "EXPLAIN ANALYZE in PostgreSQL vs what I was doing in SQL Server"
date: 2014-07-19
author: marcetux
tags: [postgresql, sql, databases, performance]
---
The query analysis work from SQL Server translated into PostgreSQL mostly intact but with
different commands and different output. The execution plan habit — read the plan before
guessing — is the right instinct in both databases. The PostgreSQL spell is `EXPLAIN
ANALYZE`, and the output is worth learning to read.

`EXPLAIN` shows the planned execution without running the query. `EXPLAIN ANALYZE`
runs it and shows actual versus estimated row counts, which is the important comparison.
When the planner estimates 10 rows and sees 100,000, the index it chose for 10 rows is
wrong for 100,000, and the plan diverges from optimal. The sequence scan with high
actual row counts and a low estimate is the PostgreSQL equivalent of the SQL Server
clustered index scan I was chasing in the execution plan work from February last year.

The specific thing that bit me on the feeds query was stale statistics. The planner
estimates row counts from statistics — table samples gathered by `ANALYZE`. A table
that grew fast without an `ANALYZE` run has stale statistics and the planner makes bad
choices. Running `ANALYZE match_events` after the table tripled in size changed the plan
from a sequential scan to an index seek, two seconds to twenty milliseconds. The
knowledge transferred; only the syntax changed.
