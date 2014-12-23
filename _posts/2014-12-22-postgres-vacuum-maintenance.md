---
layout: post
title: "PostgreSQL VACUUM and table bloat in a write-heavy app"
date: 2014-12-22
author: marcetux
tags: [postgresql, databases, devops, performance, operations]
---
An alert fired this month on the `match_events` table size — it grew by forty percent
in a week without a corresponding growth in the logical row count. The table has a lot
of DELETE and UPDATE activity: events are created when matches happen, updated when users
view or respond, and deleted after a retention window. PostgreSQL's MVCC model means
deleted rows aren't immediately freed — they're marked dead and left in place until
VACUUM reclaims them. That delay is called table bloat.

PostgreSQL runs autovacuum automatically, but the default configuration is conservative
— it runs when the dead tuple count exceeds a threshold as a percentage of the table
size. A table that grows faster than autovacuum's defaults keep up accumulates bloat.
The fix is tuning the autovacuum parameters for high-churn tables: `autovacuum_vacuum_scale_factor`
down from the default 0.2 to 0.05, so vacuum runs when five percent of rows are dead
instead of twenty. The table-level setting overrides the default without touching the
global config.

VACUUM itself is non-blocking for reads and writes in most cases; `VACUUM FULL` is the
exception and should be reserved for emergencies (it rewrites the table and holds an
exclusive lock). The immediate fix for the bloat was a manual `VACUUM ANALYZE match_events`;
the table shrank by thirty percent and the query planner statistics refreshed. The
long-term fix was the autovacuum tuning. Autovacuum is a background janitor; configure
it for your workload, not the default workload it was designed to handle.
