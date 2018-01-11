---
layout: post
title: "GraphQL and the API contract question"
date: 2018-01-10
author: marcetux
tags: [graphql, api, rest, architecture, backend]
---
The mobile team came to me with a familiar complaint: every screen needs a slightly
different shape of data, and REST gives them either too much in one fat endpoint or
too many round trips in a bunch of small ones. We prototyped a GraphQL layer over the
existing services for the patient scheduling flow, and the conversation went exactly
as advertised in the spec, and I'm still not entirely sold for every use case.

The schema is the thing that actually works. Defining every type and field in SDL is
not just documentation — it's a contract the tooling enforces, and the iOS and Android
teams can generate their own types from it. That's a real win over the "go read the
Confluence page" workflow we had before. Introspection means clients can discover
what's possible without me keeping a separate postman collection current.

What gives me pause is the complexity at the server boundary. N+1 queries are not a
GraphQL problem per se, but GraphQL makes them embarrassingly easy to produce, and
DataLoader adds its own mental model. For a public API with unpredictable clients I'd
lean toward it. For a small team where I know both sides of the wire, a
well-structured REST API with consistent pagination conventions costs less to operate.
The tool is genuinely good; just be sure it's solving the problem you have, not the
problem the post title implies.
