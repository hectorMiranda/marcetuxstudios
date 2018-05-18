---
layout: post
title: "SQL query tuning on a legacy reporting database"
date: 2018-05-17
author: marcetux
tags: [sql, sqlserver, performance, databases, backend]
---
The reporting tier review I picked up in week one turned out to be a straightforward
query-tuning exercise dressed up in a lot of legacy complexity. The six-hour jobs were
doing six-hour work, but the actual database time was about ninety minutes — the rest
was row-by-row processing in a stored procedure that moved data through several temp
tables, materializing intermediate results that SQL Server would have handled fine in a
single set-based operation.

The big wins came from two changes. First, the booking aggregation query had a cursor
that iterated one row at a time and called a scalar function per row — the scalar
function was doing a lookup that the query could have done with a join. Replacing the
cursor with a set-based query and the function call with an indexed join brought
that step from forty-five minutes to under three. Second, there was an implicit type
conversion in a `WHERE` clause — a `varchar` column compared against a numeric literal
— that was suppressing index seeks across the largest table. Making the type explicit
recovered the index.

The lesson is the same as it always is with SQL performance: find the most expensive
operator in the plan, understand why it's doing what it's doing, and fix the query
before adding an index. Indexes are the tool you reach for after the query is correct.
The reporting window is now comfortably inside three hours.
