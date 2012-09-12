---
layout: post
title: "Indexing for reporting queries in SQL Server"
date: 2012-09-11
author: marcetux
tags: [sqlserver, performance, databases, indexing]
---

A reporting query that scanned a few hundred million rows went from "go get
coffee" to sub-second this week, and the fix was unglamorous: the right index plus
covering columns.

The query rolls up bandwidth by customer and day over a date range. The original
plan was a clustered index scan — the database was reading the entire table to
answer a question about a slice of it. The win was a nonclustered index on
`(CustomerId, Date)` with the measured columns tacked on as `INCLUDE`d columns so
the query never has to jump back to the base table.

That `INCLUDE` trick is the part people miss. A **covering index** means every
column the query touches lives in the index itself; SQL Server answers entirely
from it. You trade some write cost and disk for read speed — exactly the trade you
want on a reporting table that's written by a batch job and read by humans all day.

The discipline: read the actual execution plan, don't guess. The optimizer will
tell you it wanted an index it didn't have. Believe it before you start adding
hints.
