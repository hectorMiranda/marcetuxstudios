---
layout: post
title: "Couchbase document modeling at Spark"
date: 2014-07-02
author: marcetux
tags: [couchbase, nosql, databases, architecture, spark-networks]
---
The Spark data layer has two databases running side by side: PostgreSQL for the relational
user graph — who matched whom, who messaged whom, subscription history — and Couchbase
for the profile data itself: interests, photos, answers to personality questions. The
split is intentional; the profile data is highly variable between users and changes
frequently, where the relational graph is stable and needs joins.

In Couchbase, the document ID is the unit of caching. Because Couchbase is memcached-
compatible, a profile document read from disk gets cached in memory and served from
cache on subsequent reads until it expires or is updated. The hot profiles — frequently
viewed users — stay in RAM automatically. The cold profiles are on disk and slower, but
the cache absorbs the load where it matters most. You get caching and persistence from
one system with one API.

The modeling decision that keeps coming up is granularity: one big document per user, or
many small documents. Big documents read fast when you need the whole profile; small
documents let you update one field without rewriting the whole blob and reduce
conflict surface in concurrent updates. The pattern the codebase uses is medium-grained:
a core profile document, a separate preferences document, a separate photo manifest.
Each updates independently; the API composes them before returning. Reasonable.
