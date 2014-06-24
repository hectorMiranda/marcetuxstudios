---
layout: post
title: "Rails migrations and keeping them safe"
date: 2014-06-23
author: marcetux
tags: [ruby, rails, databases, postgresql, migration]
---
The first real task at Spark was adding a column and backfilling it — routine work in
any codebase, but the first time you do it in a production Rails app with millions of
rows is educational. Rails migrations are convenient enough that it's easy to miss when
a migration will take the table offline.

PostgreSQL's `ALTER TABLE ... ADD COLUMN` without a default value is near-instant — it
changes the catalog and adds a null value for the column logically without rewriting rows.
Adding a default is different: older PostgreSQL versions rewrote every row. The safe
pattern is to add the column nullable, deploy the code that writes the new column, backfill
the existing rows in batches in a separate migration, add the NOT NULL constraint, and
then set the default. It's five steps instead of one, but each step is non-locking.

The migration that writes in batches is the one that surprised me — the batch size
matters. A single UPDATE on millions of rows holds a table lock for minutes. Batches of
a thousand rows, committed individually, run in milliseconds each and can be interrupted
without leaving the table locked. Rails doesn't enforce the batching pattern — that's
engineering judgment. The convenience of `add_column` is real; knowing when it's
dangerous is the part you add yourself.
