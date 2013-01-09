---
layout: post
title: "API versioning without regret"
date: 2013-01-08
author: marcetux
tags: [rest, api, versioning, http]
---
Once an integrator depends on your API, you can't change it — you can only add a new
version and keep the old one alive. We're about to make a breaking change, so I
spent a day reading how everyone else handles versioning and picking a lane.

Three common approaches. **URL versioning** (`/v1/report`) — ugly to purists,
trivial for clients and proxies, impossible to get wrong. **Header versioning** (a
custom header or a vendor media type in `Accept`) — keeps URLs clean and is more
"correct," but it's invisible, harder to test in a browser, and surprises people.
**No versioning, just don't break things** — aspirational and occasionally real if
you only ever add optional fields.

I'm going with URL versioning despite the aesthetic complaints, for one reason: it's
the one a tired integrator at 5pm cannot misuse. Clean URLs are a luxury; a version
scheme nobody mis-implements is a feature. Boring at the boundary, again.
