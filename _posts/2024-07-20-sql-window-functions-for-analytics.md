---
layout: post
title: "SQL window functions for analytics you stopped writing subqueries for"
date: 2024-07-20
author: marcetux
tags: [sql, analytics, postgres, databases, performance]
---
A tutoring student showed me an analytics query that was four levels of nested
subqueries deep. Correct output, unreadable logic, mediocre performance. I rewrote
it with window functions and the student thought I was using a different language.
They'd learned SQL from tutorials that stop at GROUP BY and never mention OVER.
This is the gap that produces unnecessarily complex queries for years.

Window functions aggregate or rank across a set of rows related to the current row
without collapsing those rows the way GROUP BY does. `ROW_NUMBER() OVER (PARTITION
BY customer_id ORDER BY created_at)` assigns a sequential number to each row
within each customer's set. `LAG(amount, 1) OVER (ORDER BY date)` reads the
previous row's amount for the current row. `SUM(amount) OVER (PARTITION BY month
ORDER BY date ROWS UNBOUNDED PRECEDING)` computes a running total. Each of these
replaces a self-join or subquery with a single readable clause.

The performance aspect is real too: the query planner can push window functions
through indexes more efficiently than equivalent self-joins in many cases. On the
student's query, the rewrite went from a four-second full-table-scan query to a
sub-second indexed scan. The logic was also now readable on a single pass. Window
functions are not an advanced feature; they're the feature you reach for when the
GROUP BY version produces a subquery. PostgreSQL has had them since 8.4. There is
no excuse to still write the nested version.
