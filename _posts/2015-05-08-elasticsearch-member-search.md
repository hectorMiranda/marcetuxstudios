---
layout: post
title: "Elasticsearch for the member search bar"
date: 2015-05-08
author: marcetux
tags: [elasticsearch, search, rails, backend]
---
Full-text search on member profiles — searching by nickname, city, description —
was built on Postgres `ILIKE` patterns, which works until it doesn't and then explains
itself clearly: sequential scan, no tokenization, no relevance ranking, slow. We
switched to Elasticsearch and it's the kind of change where the before looks
embarrassing in retrospect.

Elasticsearch indexes documents, not rows — you push a JSON member representation,
it indexes the text fields with a configured analyzer, and queries come back with a
relevance score. The query DSL is verbose but expressive: a `multi_match` query across
`nickname`, `city`, and `headline` with field boosts (`nickname^3`) ranks exact
nickname matches higher than city matches. The results look right to a user in a way
that `ILIKE '%carlos%'` never quite did.

The operational side is straightforward at our scale — a single-node cluster on an EC2
instance, indexed data replicated nightly from Couchbase via a small Ruby sync script.
The search index is eventually consistent with the primary store, which is fine: a new
member visible in search within a minute of signup is well within user expectations.
The Couchbase documents stay the source of truth; Elasticsearch is a read-side index
that exists to serve fast queries. Source of truth and query index are different jobs;
separating them is the right call.
