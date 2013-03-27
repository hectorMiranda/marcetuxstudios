---
layout: post
title: "Sane SQL pagination with ROW_NUMBER"
date: 2013-03-26
author: marcetux
tags: [sql, sqlserver, performance, databases]
---
A report table in the portal had been paginating with `TOP N` and a filter on the last
seen ID, which works until the ordering isn't by ID and suddenly you're re-deriving
the offset by counting rows. The right answer in SQL Server is `ROW_NUMBER() OVER
(ORDER BY ...)`, and it's been there since 2005 — I just never had the pain point
sharp enough to switch.

The pattern is a CTE or subquery that adds a row number to every result row, partitioned
however the query is already ordered. Then the outer query filters `WHERE RowNum BETWEEN
@Start AND @End`. The database does the math, not the application. Change the page, send
different `@Start`/`@End` values — same query, no cursor, no fragile ID-based gymnastics.
The execution plan stays sane because the optimizer can use an existing index for the
order; it's not scanning and counting.

The minor friction is that `ROW_NUMBER` reruns if the underlying data changes between
pages, which means a user can see a row twice or miss one if a new row inserts mid-page.
For this report that's fine — it's not a real-time feed, and showing slightly stale
ordering beats the alternative of either keyset pagination complexity or fetching the
whole result set at once. Pick the right tradeoff for the data's actual nature.
