---
layout: post
title: "SCIM 2.0 and the boring parts that matter"
date: 2026-01-07
author: marcetux
tags: [identity, scim, provisioning, api, dotnet]
---
SCIM 2.0 is one of those standards that looks simple until you have to implement it correctly. The spec gives you a clean resource model — Users, Groups, a `/ServiceProviderConfig` endpoint — and a consistent JSON schema. The happy path is genuinely pleasant. It's the edge cases where implementation teams start improvising, and the improvised edges are where provisioning breaks in silent, expensive ways.

The part I keep coming back to is idempotency. A SCIM PUT is supposed to be idempotent — apply the same payload twice and you get the same result. But if your update handler does a diff-then-patch internally without checking whether the underlying record already matches the desired state, you end up generating spurious audit events or, worse, triggering downstream sync operations for a "change" that was actually a no-change. The spec tells you what the contract is; it doesn't tell you that the thing listening to your events will panic if you emit the same change twice. I learned that the hard way at City National, and I'm seeing the same pattern here.

The other piece I'm watching is error shape. SCIM defines a `scimType` error response — `uniqueness`, `mutability`, `invalidValue` — but most consumers only handle HTTP status codes and assume the body is an afterthought. The right call is to be correct on both dimensions: proper status code and a body the spec actually describes. Boring at the boundary, as always. The client that works is the one that got the boring parts right.
