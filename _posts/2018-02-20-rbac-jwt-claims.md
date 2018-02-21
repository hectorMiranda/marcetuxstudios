---
layout: post
title: "RBAC with JWT claims instead of a permissions table"
date: 2018-02-20
author: marcetux
tags: [security, jwt, authentication, aspnet, architecture]
---
The Go RN platform has three kinds of users — providers, clinic staff, and admins —
and they see and can do meaningfully different things. The first version wired
permissions to a database table that every request joined against. That join is in
the path of every authenticated call, and it showed up in our latency traces more
than it deserved to.

The replacement is JWT claims. When the user authenticates, the token issuer embeds
the role and a handful of fine-grained permission claims directly in the signed token.
The API validates the token's signature and reads the claims — no database round trip,
no cache to invalidate. Authorization decisions become policy checks against claims the
token already carries. In ASP.NET Core the policy system makes this readable: define
a policy by name, register it once, decorate endpoints with `[Authorize(Policy = "...")]`.

The trade-off worth knowing: claims are baked into the token at issuance. If you
revoke a permission, the old token is still valid until expiry. Short-lived tokens
(15 minutes) with a refresh flow shrink that window to acceptable. For our use case —
clinic staff with stable roles — the window was fine and the latency improvement was
worth it. For a system where permissions change in real time, you'd want an additional
check at the resource boundary.
