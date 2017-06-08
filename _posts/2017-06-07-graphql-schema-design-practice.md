---
layout: post
title: "GraphQL schema design in practice"
date: 2017-06-07
author: marcetux
tags: [graphql, api, architecture, nodejs]
---
The GraphQL proof-of-concept from February is growing into something the team actually
wants to ship for the seller-facing API, which means moving from "this works in demo"
to "this is designed." The schema design decisions I got wrong in the PoC are visible
now that real query patterns are landing, and I want to document what I'd do differently.

The main mistake: modeling the schema after the database tables rather than the use
cases. A product has many variants; variants have many channel listings; each listing has
a status. I modeled three types and three connection edges, which is correct but
verbose for the most common query: "give me this product and its current status on all
channels." A type with a computed field that flattens the nested data — `channelStatus`
returning a map keyed by channel ID — serves that query in one resolver level instead
of three. The schema should model what clients actually ask for.

The practical design rule I'm now applying: write the queries first, then design the
schema that serves them efficiently. The schema isn't the data model — it's the API
contract, and it should be shaped around the consumers' access patterns. Mutations
follow the same logic: model the user intent, not the table update. `setInventoryCount`
is a better mutation than `updateProductField(field: "inventoryCount", value: "42")`.
Naming things like operations rather than generic setters makes the schema self-
documenting.
