---
layout: post
title: "Multi-tenant identity patterns for B2B systems"
date: 2026-03-09
author: marcetux
tags: [identity, multi-tenant, auth0, architecture, dotnet]
---
AmaWaterways sells river cruises through a mix of channels: direct guests, travel agents, and large B2B accounts that are themselves agencies with multiple agents. That structure means identity isn't flat — a principal belongs to a tenant (the agency), and the tenant has its own configuration, branding, and permission scope within the platform. The guest who booked directly also has a tenant context; it just happens to be the "AmaWaterways direct" tenant.

The decision point that matters most early in a multi-tenant identity design is where tenant resolution lives. There are three common answers: in the token (a tenant claim), in the URL (subdomain or path prefix), or in a lookup keyed to the user's identity. Each has tradeoffs. Token-in-claim is fast and requires no extra lookup per request, but tenant claims in a JWT go stale when a user's tenant membership changes and the token hasn't been refreshed. URL-based resolution is explicit and easy to debug, but breaks the single-URL experience some products need. A hybrid works well for the AmaWaterways case: the tenant ID lives in the token for efficiency, but the enrichment middleware validates it against current data once per session and updates the context if the claim is stale.

The other multi-tenant pitfall is data isolation mistakes — queries that accidentally return records across tenants because someone forgot a `WHERE tenant_id = @tenantId`. The solution I've standardized on is a tenant context service injected into the data layer, with a repository base class that applies the tenant filter automatically. Opt-out requires an explicit method override that's obviously named and obviously dangerous. The default is safe; being unsafe requires intentional work.
