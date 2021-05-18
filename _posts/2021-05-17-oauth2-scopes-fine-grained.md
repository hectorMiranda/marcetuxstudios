---
layout: post
title: "OAuth2 scopes and the granularity question"
date: 2021-05-17
author: marcetux
tags: [security, oauth2, api, architecture]
---
The access-token review this quarter found scopes that were too coarse. The pattern
was a single `api:write` scope that gated every mutation across the API — meaning
a client with permission to update a customer's mailing address also had permission
to close an account. Both operations required `api:write`. The scope was accurate
in that it allowed writes; it was useless as an access-control boundary because
it allowed all writes.

The right granularity for scopes is roughly: one scope per resource type, split by
action class. `customers:read`, `customers:write`, `accounts:close`. A client that
processes address updates gets `customers:write` and nothing else; a service that
needs to close accounts gets `accounts:close` and not `customers:write`. The scopes
name what the client is authorized to do, not what it's authorized to be. A
third-party integration asking for `api:write` is asking for the keys to the
building; a third-party integration asking for `reports:read` is asking for the
key to one room.

The migration is painful because every existing client needs to be re-authorized
with a new scope list, and the authorization server and resource servers need to
enforce the new scopes consistently. We're doing it incrementally: new endpoints
get fine-grained scopes from day one, existing endpoints get a deprecation period
where both the old coarse scope and the new fine-grained scope are accepted, then
the coarse one disappears. Boring migration, real security improvement.
