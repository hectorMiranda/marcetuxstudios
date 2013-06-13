---
layout: post
title: "Web API 2 and OData-lite queryable endpoints"
date: 2013-06-12
author: marcetux
tags: [dotnet, webapi, odata, rest, aspnet]
---
Web API 2 is still in preview but I've been building against it for the reporting
endpoints, and the `[Queryable]` attribute (technically `EnableQueryAttribute` in the
final naming) is the feature that earned its keep this week. The report grid has eight
different filter combinations — by date range, customer tier, region, threshold — and
with `[Queryable]` on the action returning `IQueryable<ReportRow>`, the framework
translates OData `$filter`, `$orderby`, `$top`, `$skip` query parameters into LINQ
expressions against the underlying query.

That means eight distinct controller actions collapsed into one, and the client can
express its own combinations without me wiring each one. The framework validates the
query parameters and rejects malformed expressions with a proper `400`. Adding a new
filter field means adding the property to the model class; no new endpoint, no new
route.

The caution I exercise: I don't expose `IQueryable` directly to arbitrary user-controlled
queries on large tables without limits. `MaxTop` is set to 500 on any endpoint touching
the bandwidth data; unauthenticated callers can't reach these endpoints at all. The
`[Queryable]` attribute is a productivity tool for known callers with understood data
shapes — it's not a replacement for thinking about what a query will do against a table
with eight million rows. Use it for internal or partner APIs; think harder before
exposing it publicly.
