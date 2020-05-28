---
layout: post
title: "Idempotent APIs and why banking makes you care"
date: 2020-05-27
author: marcetux
tags: [api, architecture, banking, dotnet]
---
Network transience makes the idempotency problem unavoidable in banking. A payment
POST succeeds on the server, but the client's TCP connection drops before it receives
the 200. The client retries. Now you have two payments, or an error, depending on
whether the server side is naive or careful. In retail software that's annoying; in
payment processing it's a compliance event.

The pattern is an idempotency key — a GUID the client generates and includes in the
request header or body. The server stores it alongside the result. On retry with the
same key, the server returns the stored result without executing the operation again.
The key's lifetime and storage need a little thought: Redis with a TTL that matches
your retry window works well. If the key exists and the operation succeeded, return the
cached result. If the key exists and the operation failed, you can either return the
failure or retry-with-idempotency depending on the failure type. If the key doesn't
exist, execute and store.

The elegance is that the client doesn't need to know whether the response is fresh
or cached — it gets the same answer either way. The complexity budget is all on
the server side, where it belongs. For services that touch money, the extra fifteen
lines of middleware are the cheapest insurance in the codebase.
