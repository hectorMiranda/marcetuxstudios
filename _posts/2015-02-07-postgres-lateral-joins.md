---
layout: post
title: "Postgres LATERAL joins for per-row subqueries"
date: 2015-02-07
author: marcetux
tags: [postgresql, sql, databases, backend]
---
A recommendation query needed the top-three matches for each member in a batch — not
the global top three, but the best three *per member*. The first version used a
correlated subquery, which works and is also a full scan of the candidates table for
every row in the member batch. It was the kind of correct-but-slow that gets noticed.

Postgres's `LATERAL` keyword is the fix: it lets a subquery in the `FROM` clause
reference columns from an earlier table in the same `FROM`, so you get a correlated
subquery that the planner can optimize as a join rather than running it row by row
naively. Wrapped with `ORDER BY` and `LIMIT 3`, the lateral subquery returns exactly
the three best candidates per member and the engine knows that's the shape — it can
use the index properly and stop looking early.

The explain plan before and after are almost comical in how different the estimated
costs are. Not every query has a lateral lurking in it, but the pattern shows up
whenever you need "the N best of something, broken down per outer row." It's been in
Postgres since 9.3 and I'd been sleeping on it — the correlated-subquery habit is
strong. Now I check the explain plan for nested loop fan-out before I call a query fast.
