---
layout: post
title: "Entity Framework no-tracking queries for read-only endpoints"
date: 2013-10-16
author: marcetux
tags: [dotnet, entityframework, performance, databases, webapi]
---
A performance audit of the reporting endpoints showed Entity Framework spending
meaningful time on change tracking — maintaining internal state so it can detect
modifications and produce an UPDATE statement when `SaveChanges()` is called. For
endpoints that return data and never modify it, that work is pure waste.

The fix is `AsNoTracking()`: chain it on any LINQ query that's purely for reading.
`context.Customers.AsNoTracking().Where(c => c.IsActive).ToList()`. The returned entity
objects are disconnected from the `DbContext` — EF allocates less memory, skips the
snapshot comparison, and returns faster. On a query returning a few hundred rows, the
difference is measurable in a profiler; on a query driving a paginated report over
thousands of rows it's noticeable to a user.

The discipline is to make the intent explicit. Read-only endpoints should use
`AsNoTracking` every time; write paths should not. The wrong call is accidentally
returning a tracked entity from a read endpoint and having it stay attached to the
context longer than intended. I added a wrapper around my repository's read methods
that enforces `AsNoTracking` — the application code can't accidentally omit it for a
read query because the read method doesn't offer a tracked variant. The interface
encodes the constraint.
