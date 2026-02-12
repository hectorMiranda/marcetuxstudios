---
layout: post
title: "Idempotent APIs in practice beyond the theory"
date: 2026-02-11
author: marcetux
tags: [api, idempotency, dotnet, distributed-systems, identity]
---
Every API design talk covers idempotency in the abstract — "use idempotency keys, PUT is idempotent, design your endpoints to be safely retried." The theory is correct and the advice is sound. What the talks rarely cover is the implementation detail that makes idempotency actually hold under the conditions where it matters most: concurrent retries from an impatient client.

The naive approach is to check whether the resource already exists, and if so return it. The problem is that "check then act" is a race condition when two requests for the same idempotency key arrive within milliseconds of each other — which is exactly when a client's retry logic fires. Both requests check, both find nothing, both proceed, and one fails with a uniqueness constraint violation that surfaces as a 500 to the client. The fix is to do the uniqueness check at the database layer (a unique index on the idempotency key column) and handle the specific constraint violation by fetching and returning the already-committed record. The database is the right place to serialize that conflict because it can actually do it atomically.

The second detail is idempotency key scope. Keys should be scoped to the operation type, not just globally unique. A key that worked for a user create should not silently match a user update that happened to use the same UUID. If your idempotency log stores the operation type alongside the key, a mismatch becomes a detectable error rather than a silent wrong answer. More moving parts, but moving parts that are individually testable and that fail loudly instead of quietly.
