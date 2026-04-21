---
layout: post
title: "SCIM filtering and pagination the spec gets right"
date: 2026-04-20
author: marcetux
tags: [identity, scim, api, dotnet, provisioning]
---
One of the parts of SCIM 2.0 I've come to respect more the longer I work with it is the filtering and pagination design. Most provisioning integrations don't need it — they bulk-sync everything on schedule — but when a downstream system has a hundred thousand users and needs to reconcile the delta from the last sync without pulling the entire dataset, the SCIM query model earns its complexity.

The filter syntax is where the spec made a good call: it defines a small, structured expression language rather than allowing arbitrary query strings. Filters like `userName eq "hector.miranda"` or `meta.lastModified gt "2026-01-01T00:00:00Z"` are straightforward to parse and index against. The alternative — freeform query strings that each server interprets differently — is what REST APIs that don't specify filtering end up with, and the result is always a mess of undocumented behaviors that clients have to reverse-engineer. The SCIM filter grammar is just structured enough to be implementable and just limited enough to be consistently implementable.

Pagination via `startIndex` and `count` is less elegant — it's cursor-free, which means pages can shift if records are inserted during a paginated traversal — but the spec acknowledges this and notes that clients should treat paginated results as an approximate snapshot rather than a consistent view. For identity data, where the sync job runs periodically and a few duplicates in the result are handled by idempotent processing, that's an acceptable tradeoff. The right fix for production workloads with active writes during sync is cursor-based pagination, which the spec doesn't prohibit — it just doesn't require it. We added it as an extension and document it in our service provider config.
