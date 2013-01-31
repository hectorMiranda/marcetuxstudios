---
layout: post
title: "A working survey of NoSQL"
date: 2013-01-30
author: marcetux
tags: [nosql, mongodb, couchbase, redis, databases]
---
"NoSQL" gets used as if it's one thing, and after a year of actually using several I
want to write down that it's at least four different things solving different
problems.

**Key/value** (Redis) — blazing, simple, perfect as a cache or for ephemeral state;
not where your system of record lives. **Document** (MongoDB, Couchbase) — store
whole JSON-ish objects, great when your data is naturally a document and your access
is by id; you trade joins and some consistency for flexibility and scale.
**Column-family** (Cassandra, HBase) — built for enormous write volume across many
machines; overkill until you genuinely have that problem. **Graph** (Neo4j) — when
the relationships *are* the data.

The mistake I watch teams make is reaching for NoSQL as a fashion statement and then
reimplementing joins and transactions badly in application code. SQL Server is still
the right default for relational, transactional data — which is most business data.
Reach for NoSQL when your data's *shape* or *scale* actually fights the relational
model, not because it's the new thing.
