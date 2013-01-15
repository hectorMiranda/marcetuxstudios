---
layout: post
title: "SQL Server window functions finally click"
date: 2013-01-14
author: marcetux
tags: [sqlserver, sql, databases, reporting]
---
I've been doing in application code what SQL Server 2012's window functions do in
one line, and I'm a little embarrassed about it. Running totals, ranks, and
moving averages over the bandwidth data — all of it expressible in the query.

The unlock is `OVER (PARTITION BY ... ORDER BY ...)`. A running total of bytes per
customer over time is `SUM(Bytes) OVER (PARTITION BY CustomerId ORDER BY Date)`.
"Each customer's biggest day" is `ROW_NUMBER() OVER (PARTITION BY CustomerId ORDER BY
Bytes DESC)`. The work that used to be a self-join or a loop in C# is now declarative
and the optimizer does it once.

`LAG` and `LEAD` are the other revelation — "how much did this day differ from the
day before" without joining the table to itself on `Date - 1`. I'm rewriting a pile
of reporting code to push this work into the database where it belongs, and the C#
that's left is just shuttling the answer to the client.
