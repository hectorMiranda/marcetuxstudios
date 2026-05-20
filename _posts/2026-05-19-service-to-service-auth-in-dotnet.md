---
layout: post
title: "Service-to-service auth in a .NET identity platform"
date: 2026-05-19
author: marcetux
tags: [identity, dotnet, auth0, security, api]
---
Within the AmaWaterways identity platform, not all callers are humans. The booking service, the reporting service, and the CRM integration all call identity APIs to provision users, check permissions, or synchronize data on a schedule. These machine-to-machine calls have a different auth requirement than user-interactive flows — there's no human to redirect to a login page, and the credential is a secret managed by the service, not a password typed by a person.

The right mechanism for this is client credentials flow: the calling service authenticates to Auth0 using a client ID and client secret, receives a short-lived access token scoped to the specific API it needs to call, and presents that token on every request. The identity service validates the token the same way it validates user tokens — signature, expiry, audience, scope — and the authorization logic doesn't have to care whether the principal is a human or a service. This symmetry is the payoff of token-based auth: the token is the auth context regardless of how it was obtained.

The implementation details that matter: the client secret should live in Azure Key Vault and be rotated on a schedule, not in an environment variable that gets committed to a config file. The machine-to-machine tokens should have a narrow scope — the exact permissions the service needs, not a superuser scope that covers everything. And the token should be cached by the calling service until it expires, not fetched on every request, because a round-trip to Auth0 on every API call is unnecessary latency and unnecessary load. Cache the token, check expiry before use, refresh proactively. The pattern is identical to how a browser client handles it; the implementation is just on the server side.
