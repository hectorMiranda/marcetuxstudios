---
layout: post
title: "Zero-downtime database migrations on a live schema"
date: 2021-08-23
author: marcetux
tags: [databases, devops, postgres, architecture]
---
A schema migration that required renaming a column caused a 45-minute maintenance
window that we'd told stakeholders would be zero-downtime. The migration ran fine
in staging where there was no concurrent traffic. In production, the rename held
an `ACCESS EXCLUSIVE` lock on the table for the duration, and the application
traffic that tried to read or write to that table queued behind the lock until the
rename finished. The migration was fast; the lock queue was not.

Zero-downtime schema migrations require a different workflow than "run the migration,
hope it's fast." The expand-contract pattern is the way: first add the new column
alongside the old one, update the application to write to both and read from the
new one, backfill historical data in small batches, then remove the old column in
a separate migration once the application no longer touches it. Three migrations
where you'd have one, and zero lock contention because you're never blocking
existing reads while you migrate.

The column rename specifically can also be done with a view — keep the old column
name in a view while the physical column uses the new name — but the expand-
contract pattern generalizes to any schema change: new tables, dropped columns,
changed data types. The discipline is that the application and the database can
never be in a state where the application assumes the new schema and the old schema
is still in place. Each step of the migration must be backward-compatible with the
previous application version. Two deployments, no lock, no window.
