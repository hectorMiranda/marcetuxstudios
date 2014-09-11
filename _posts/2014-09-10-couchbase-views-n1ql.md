---
layout: post
title: "Couchbase views versus N1QL for secondary queries"
date: 2014-09-10
author: marcetux
tags: [couchbase, nosql, databases, n1ql, spark-networks]
---
Three months with Couchbase and I've used both map-reduce views and N1QL queries, and
the difference matters enough to think about carefully before adding a query to the
codebase. Views are precomputed indexes: a map function extracts index keys from
documents, the index is maintained incrementally, and queries against the view are fast.
N1QL is ad-hoc: you write SQL-like syntax and Couchbase parses and executes it against
the documents.

The performance difference is significant for high-traffic queries. A view index is
maintained as documents change; the query result is essentially a lookup into a B-tree.
N1QL with a secondary index is also efficient for supported operations, but the index
coverage is less complete than a hand-crafted view. For the profile search — find
members matching age range, location, and interests — a view index tuned for those
dimensions runs faster than N1QL against the same data.

Where N1QL wins is development iteration. Writing and modifying map-reduce views requires
publishing new design documents and waiting for the index to build, which is slow when
the dataset is large. N1QL lets you experiment with queries interactively against live
data. My workflow is: use N1QL to understand the query shape, convert to a view index for
any query in a hot code path. The development experience of N1QL; the production
performance of a view.
