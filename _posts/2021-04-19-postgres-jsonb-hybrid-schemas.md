---
layout: post
title: "Postgres JSONB for hybrid rigid and flexible schemas"
date: 2021-04-19
author: marcetux
tags: [postgres, databases, architecture, design]
---
A new product configuration table had a core set of fields that every record
shares — effective date, product code, owner — and a metadata section that
varies wildly by product type. The team's first instinct was a separate
configuration_attributes table with key/value rows. I've been down that road
enough times to know where it ends: queries that join back to the EAV table
and reconstruct the shape in application code, and a schema that tells you
nothing about what's actually stored in it.

The better answer for Postgres is a JSONB column alongside the relational columns.
The fixed schema stays relational and typed; the variable part goes in JSONB where
Postgres stores it as binary and lets you query, index, and filter against it with
full operator support. `@>` containment checks, GIN indexes on specific paths, and
`jsonb_set` for updates — it's not NoSQL bolted on; it's a first-class column type
with real indexing semantics.

What convinced me was `CREATE INDEX ON product_config USING GIN ((metadata -> 'rateClass'))`.
That index makes a query on a JSON field as fast as a query on a dedicated column,
and I don't need a schema migration every time a product team adds a metadata key.
The discipline is keeping the truly fixed data relational — where the constraints
and foreign keys live — and using JSONB only for the genuinely variable part. Reach
for JSONB when the schema variation is real, not when you're avoiding the design
conversation.
