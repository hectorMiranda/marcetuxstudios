---
layout: post
title: "Why parameterized queries matter for plan caching"
date: 2013-05-14
author: marcetux
tags: [sql, sqlserver, performance, databases]
---
A dashboard query that had been fast for months suddenly started taking four seconds
every few requests and one millisecond the rest of the time. The execution plan was
fine on inspection. The RECOMPILE option forced it back to fast, which was a clue that
something about parameter sniffing or plan cache poisoning was going on.

The root cause turned out to be a handful of places in the codebase doing string
interpolation instead of parameterization: `"WHERE CustomerId = " + id`. SQL Server
hashes the text of the query to find a cached plan. A query with a literal 12 is a
different hash from a query with a literal 1247, so the plan cache grows one entry per
distinct value, each compiled cold the first time. With parameterized queries —
`WHERE CustomerId = @id` — all calls share the same plan entry.

The fix was global and a little embarrassing: grep for string concatenation in SQL
query construction and replace with parameterized `SqlCommand`. Every place. Twelve
sites in the codebase, none in code written after last year, all in the older
reporting layer I hadn't touched. The performance improvement was immediate and the
security improvement (SQL injection surface area) came along for free. This is one of
those where the correctness reason and the performance reason both point the same way;
that alignment usually means it's actually the right approach.
