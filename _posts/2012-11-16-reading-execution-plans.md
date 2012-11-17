---
layout: post
title: "Reading SQL Server execution plans"
date: 2012-11-16
author: marcetux
tags: [sqlserver, performance, databases]
---
A follow-up to the indexing post, because "add an index" is the *answer* and the
execution plan is how you find the *question*. I spent an afternoon teaching a
teammate to read plans and it's worth writing the short version down.

Turn on the actual (not estimated) plan and read it right-to-left, top-to-bottom —
that's the order operations actually run. The things that should make you wince:
**Index Scan** or **Table Scan** where you expected a Seek (you're reading the whole
table to answer a narrow question), and fat arrows between operators (lots of rows
moving — a join or filter that should have happened earlier).

The single most useful habit: hover the operator and read the row counts. A plan
where *estimated* and *actual* rows diverge wildly means stale statistics, and the
optimizer is making decisions on bad information. `UPDATE STATISTICS` before you go
inventing exotic indexes.
