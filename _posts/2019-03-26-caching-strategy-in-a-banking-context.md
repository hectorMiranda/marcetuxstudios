---
layout: post
title: "Caching strategy in a banking context"
date: 2019-03-26
author: marcetux
tags: [caching, redis, banking, architecture, performance]
---
Caching in a bank is a different risk conversation than caching in a product startup. Reading a stale account balance to a customer is not a minor UX quirk — it has regulatory and liability dimensions, and the team that chose the cache TTL needs to understand that. So we went through the exercise of classifying every data type by staleness tolerance before we configured a single cache entry.

The classification produced three buckets. **Reference data** — currency codes, country lists, product definitions — changes infrequently and can be cached for hours. **Derived data** — fee calculations based on account type, interest rate lookups — can be cached for minutes with a background refresh. **Balance and transaction data** — never cached past the current request context; every call goes through to the authoritative source. The categories are explicit and documented, and any engineer proposing a new cache entry has to declare which bucket it fits and get that reviewed.

The implementation uses Redis on Azure Cache, with cache keys scoped to include the requesting user's ID for any user-specific data and the data version where applicable. We set explicit TTLs and instrument cache hit and miss rates in Application Insights. When we see an unexpected miss spike it's a signal — something invalidated or expired unexpectedly. The operational discipline is the point. Caching without visibility is gambling; caching with metrics and documented classification is something I can explain to a compliance reviewer without apologizing.
