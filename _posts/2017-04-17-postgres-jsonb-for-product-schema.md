---
layout: post
title: "PostgreSQL JSONB for a flexible product attribute schema"
date: 2017-04-17
author: marcetux
tags: [postgresql, databases, schema, architecture]
---
Product attributes in marketplace integrations are a schema designer's nightmare.
Amazon's attributes for a book are completely different from its attributes for a power
tool; Walmart's required fields for apparel differ from electronics. A normalized
attribute table with a string value column and a string key column is the classic move,
and it works until you need to filter or index on attribute values, at which point it
becomes an EAV table anti-pattern with no good options.

We moved the extended attribute storage to PostgreSQL JSONB this month, and the tradeoff
feels right for our use case. A product record has a `attributes` JSONB column; each
channel's attribute set is a JSON object stored there. PostgreSQL can index into JSONB
with GIN indexes — `CREATE INDEX ON products USING gin(attributes)` — and query with
operators like `attributes @> '{"color": "red"}'` that use the index efficiently. The
schema stays loose where it needs to be loose, and PostgreSQL does the indexing work.

The thing I keep explaining to people skeptical of JSON in a relational database: JSONB
is not "no schema." It's "schema enforced by the application rather than the database
constraint." That's a tradeoff with costs — the database won't reject a bad attribute
name for you — but it's honest about what it is. Put a validation layer in front of it,
generate the JSON from typed objects, and the application is the constraint. Works for
us because we own all the write paths.
