---
layout: post
title: "Couchbase view indexes, the hard way"
date: 2015-01-03
author: marcetux
tags: [couchbase, nosql, databases, performance]
---
A matching feature on the dating side queries member profiles by distance and a handful
of preference columns. In Postgres I'd have reached for a partial index in about five
minutes. In Couchbase the equivalent is a map/reduce view, and getting it right took a
solid afternoon plus a debugging session I'm not proud of.

The view is a JavaScript map function that Couchbase runs across every document at index
time. You emit the key you want to query by — in our case a geo-bucket prefix plus age
range — and the engine builds a B-tree from those emissions. The trap I walked into:
I emitted a compound array key `[geoBucket, ageMin, gender]` and then queried a range
on just the first two, expecting it to scan the subtree. It did, but only when I set
`startkey` and `endkey` correctly — pass the wrong boundary type and you get zero rows
with no error, just silence. Reading the Couchbase docs on array key ordering fixed it,
but the feedback loop from "change map function → save → wait for reindex → query" is
brutally slow on a dataset that size.

The lesson is not that views are bad — they're actually the right place to pay the
indexing cost once instead of at query time. The lesson is to develop against a small
document sample and verify the key structure before you ever wait on a full reindex.
Build the feedback loop shorter; the tool gets nicer.
