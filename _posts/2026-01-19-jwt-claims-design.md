---
layout: post
title: "Designing JWT claims you won't regret"
date: 2026-01-19
author: marcetux
tags: [identity, jwt, rbac, security, dotnet]
---
One of the quiet architectural decisions that compounds in bad ways if you get it wrong early is what goes into a JWT. At AmaWaterways, the tokens have been growing — roles, permissions, tenant context, booking references — and we're at the point where the base64-decoded payload is getting uncomfortable in a standard auth header. It's time to be deliberate rather than additive.

The principle I keep returning to is: put in the token only what the consumer *needs at validation time without a network call*. Everything else belongs in a lookup, called once per session or request and cached. This matters because a JWT is issued at login and can't be revoked without either short expiry or a denylist — so the claims inside it are a snapshot. Stale role assignments in a long-lived token are a real access-control risk, not a theoretical one. Short expiry plus refresh is the right contract; fat tokens with everything in them are the wrong answer to the "I don't want a database call per request" problem.

For us in practice, the token carries three things: the subject identifier, the tenant identifier, and a compact RBAC role list from which permissions are derived. Permissions themselves — and anything domain-specific like booking state — come from a claims-enrichment middleware that fetches the current record once and adds it to the request context. The token stays small and accurate; the enriched context carries what the handlers need. It's more moving parts than "put it in the token," but those parts are individually testable and individually correctable without issuing new tokens to everyone.
