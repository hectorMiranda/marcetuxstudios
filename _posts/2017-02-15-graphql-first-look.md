---
layout: post
title: "GraphQL, first look from the server side"
date: 2017-02-15
author: marcetux
tags: [graphql, api, architecture, dotnet]
---
I've been watching GraphQL from the sidelines since Facebook open-sourced it in 2015,
waiting for the ecosystem to grow up before committing to it in anything serious. I
spent a few evenings building a small proof-of-concept over the seller catalog data, and
I understand the pitch now in a way that reading spec documents didn't convey.

The core thing: the client describes the shape of the data it needs, and the server
returns exactly that shape — not a REST endpoint's opinionated response, not two
round trips to assemble it. For the seller dashboard this matters because the listing
view needs product fields, pricing, channel status, and inventory in one query, and our
current REST API requires four separate calls. A GraphQL layer collapses that into one,
and the over-fetching problem — every REST call returning fields nobody asked for —
goes away by construction.

The server side is where it gets interesting. A resolver function handles each field,
and if you're not careful you end up with an N+1 query problem: every node in the graph
fires a separate database call. The solution is the DataLoader pattern — batch and
deduplicate database calls across a request. It's not automatic, you have to build it,
but once you've done it the graph becomes efficient. I'm not ready to drop REST at work,
but the catalog proof-of-concept is worth showing the team.
