---
layout: post
title: "API versioning that survives contact with clients"
date: 2017-07-03
author: marcetux
tags: [api, architecture, dotnet, design]
---
The seller-facing API at SolidCommerce is about to gain its third breaking change in
two years. The first two were handled the way most teams handle the first one: a
warning email and a cutover date that clients mostly ignored until we forced them. The
third time, I wanted a versioning strategy that didn't require an org-wide email campaign
to execute.

URL versioning — `/api/v2/products` — is what most teams reach for first and what I'd
been using. It works but it has a cost: parallel codebases. V1 and V2 of the same
endpoint share business logic but diverge in their request/response shapes, and you end
up with duplicated controllers or an indirection layer that's more complex than either
version. The alternative that's grown on me is header versioning: the client sends
`Api-Version: 2017-07-01` and the routing layer dispatches based on that. You can have
one controller with a version switch in the methods that changed, rather than two
parallel controllers.

The discipline that makes either strategy work is deprecation signaling. Every response
carries a `Deprecation-Date` header if the version the client is using is scheduled
to end. Clients that log their API responses can see the signal; clients that ignore it
will eventually get a hard error. The cutover email is the last resort, not the
strategy. Give the signal early, enforce it late, document both dates in public.
