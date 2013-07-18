---
layout: post
title: "Learning PostgreSQL on the side and what SQL Server glosses over"
date: 2013-07-17
author: marcetux
tags: [postgresql, sql, databases, learning]
---
SQL Server is what pays the bills, but I've been spending weekend time with PostgreSQL
and it keeps showing me corners of relational databases that SQL Server's tooling made
invisible. The experience is a little humbling and mostly useful.

The Postgres documentation is unusually good — the manual reads like it was written by
people who want you to understand the system, not just use it. Reading it alongside
`EXPLAIN ANALYZE` output (Postgres's equivalent of the execution plan, but in plain text
rather than a GUI) is teaching me things about how a query optimizer works that the
Management Studio plan viewer had abstracted into icons. Bitmap index scans, hash joins,
sequential scan cost estimates — these are the same concepts under SQL Server but the
Postgres planner makes them legible.

The other thing Postgres does that SQL Server charges extra for: window functions are
first-class and composable, the `RETURNING` clause on `INSERT`/`UPDATE`/`DELETE` lets
you get back the modified row without a second query, and CTEs are writable. None of
that changes how I write SQL Server code at work immediately, but understanding that
these patterns exist changes what I reach for when I'm designing a new query. Knowing
a better way to do something — even on a different platform — sharpens how you think
about doing it on the platform you actually use.
