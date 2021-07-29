---
layout: post
title: "Breaking a slow SQL query into pieces"
date: 2021-07-28
author: marcetux
tags: [sql, postgres, performance, databases]
---
A reporting query that joined seven tables and did three levels of aggregation
was timing out after sixty seconds on the production data volume. The execution
plan was a mess of nested loops and hash joins, some building on the results of
others, and the optimizer was choosing a plan that made sense for the join order
it had locked in but was wildly expensive on the actual cardinalities. The query
had grown incrementally — a join here, a filter there — and nobody had looked at
what it was doing end to end until it stopped finishing.

The approach that worked: decompose the query into stages using CTEs. Common Table
Expressions are sometimes just syntactic sugar for subqueries, but in Postgres they
have a useful property — the planner materializes them as a fence. The optimizer
can't push filters into a CTE or pull a CTE's results into a hash join the way it
can with inline subqueries. That sounds like a limitation; in this case it was a
lever. By putting the most selective filtering into an early CTE and letting
the rest of the query operate on the smaller result set, I was forcing a join order
the optimizer had been avoiding.

The query went from 60-second timeout to 800 ms. The plan is no longer optimal
in the general case — I've given up some optimizer freedom to guide the specific
path — and that's a trade I document in a comment rather than hide. Sometimes the
right move is to stop trusting the planner and show it where to go. Read the plan,
understand why the plan is wrong, and make the smallest intervention that fixes it.
