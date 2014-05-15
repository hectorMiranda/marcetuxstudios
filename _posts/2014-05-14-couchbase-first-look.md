---
layout: post
title: "Couchbase, first look during the interview process"
date: 2014-05-14
author: marcetux
tags: [couchbase, nosql, databases, architecture]
---
One of the things I had to prepare for the Spark Networks technical screen was a
conversation about their data layer, which is Couchbase — a JSON document store with a
memcached-compatible caching layer built in. I'd used MongoDB for a side project but had
never touched Couchbase, so I spent a few evenings getting familiar with the model before
the interview.

The core concept is the document: each record is a JSON blob with an ID, stored in a
bucket. There's no schema enforced at the database level — the application defines the
shape, and different documents in the same bucket can have different fields. That
flexibility is what makes document stores appealing for domains with varied record types,
like user profiles where some users have payment info and some don't. SQL handles that
with nullable columns or join tables; Couchbase just doesn't store the field.

The query story was interesting. N1QL — pronounced "nickel" — is a SQL-like language
that queries the JSON documents. That's a deliberate UX choice: people who know SQL
can query a NoSQL store without learning a new query language from scratch. The
underlying mechanism is still key-value, but N1QL with a secondary index gives you
something close to relational query power over documents. I left the conversation
wanting to use it on something real, not just understand it academically. That's usually
a good sign.
