---
layout: post
title: "Couchbase N1QL, SQL for JSON documents"
date: 2015-03-12
author: marcetux
tags: [couchbase, nosql, databases, sql]
---
One of the persistent friction points with Couchbase has been the view system — powerful
but slow to iterate on. N1QL (pronounced "nickel") is Couchbase's answer: SQL query
language for JSON documents, with secondary indexes the query planner can actually use.
It's in early access, not production-ready, but I got it running against our dev cluster
and the developer-experience difference is striking.

The query syntax is SQL with a JSON-awareness twist: `SELECT m.* FROM members m WHERE
m.ageRange[0] >= 25 AND m.location.city = 'Los Angeles'`. The `m.ageRange[0]` and
`m.location.city` are navigating into JSON array and object fields. Create a secondary
index on `location.city` and the planner uses it. The view-development loop — write
map function, wait for reindex, test — collapses to write query, execute, see results.

The caution is "early access." The query planner is young, the index coverage is
limited, and a query that needs a sequential scan across all documents will be slow in
ways that aren't obvious from the SQL syntax. I'm not migrating anything to N1QL in
production yet. But watching where Couchbase is taking this suggests that the document
database and the query model I'm comfortable with from Postgres might actually converge
into something usable. That would fix the biggest productivity gap with the document
approach.
