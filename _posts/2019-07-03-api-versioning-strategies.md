---
layout: post
title: "API versioning strategies that actually work"
date: 2019-07-03
author: marcetux
tags: [api, versioning, architecture, openapi, integration]
---
We shipped the first breaking API change in the bank's new integration layer this month, and because we'd planned the versioning strategy up front it went smoothly. The preparation was worth more than the execution. I've seen enough versioning disasters to have strong opinions about this, and the disaster is always the same: the team didn't think through versioning before they had consumers.

The strategy we landed on: URL-path versioning for the API version (`/api/v1/payments`, `/api/v2/payments`), semantic versioning for the spec file (where patch bumps are additive, minor bumps add optional fields, major bumps indicate breaking changes). The API gateway routes both versions to the appropriate service version — during a transition, v1 and v2 run concurrently. Consumers migrate to v2 on their own timeline with a published sunset date for v1. The sunset date goes into the `Deprecation` and `Sunset` response headers so any consumer who reads headers gets a machine-readable notice.

The thing I insist on: you don't change a spec, you create a new one. `payment-initiation-v1.yaml` is immutable from the moment a consumer binds to it. If I catch a mistake in v1, I document it in a change log and fix it in v2. That discipline feels painful because it means owning your mistakes publicly, but the alternative is consumers whose auto-generated clients silently break when you push a spec "fix." Immutable specs create predictable contracts, and predictable contracts create the trust that makes integration work at all.
