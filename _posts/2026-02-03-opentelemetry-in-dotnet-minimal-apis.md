---
layout: post
title: "OpenTelemetry in .NET minimal APIs without the noise"
date: 2026-02-03
author: marcetux
tags: [dotnet, opentelemetry, observability, minimal-api]
---
Wiring OpenTelemetry into a .NET 8 minimal API service is, at this point, mostly a nuget-install-and-configure exercise — the .NET runtime emits activity spans for incoming HTTP requests, outgoing HTTP calls, and database commands without any manual instrumentation. That part I had working in the first sprint. The harder part is making the resulting telemetry *useful* rather than just present, because "useful" requires discipline about what you add and what you leave out.

The thing that clicked was treating custom spans the way I treat log levels: they should tell you something you couldn't already infer from the surrounding context. Every span has a cost — ingestion, storage, rendering in Tempo or whatever backend you're using — and if your custom instrumentation is just re-stating what the framework already emits, you've paid twice for the same information. I started by tagging the identity-specific operations: SCIM provisioning requests, Auth0 token exchange, the enrichment middleware lookup. Each of those gets an activity with the tenant ID and operation type as attributes, which means I can filter and correlate across a provisioning flow without guessing which HTTP requests belonged together.

The discipline I'm enforcing on the team: no `Activity.Current?.SetTag(...)` in domain code. That coupling belongs in infrastructure adapters, not in the thing that actually does the work. The domain model shouldn't know it's being observed; the adapter layer wraps it and adds the signal. The payoff is that the observability code can evolve without touching business logic, and the domain code stays testable without mocking a tracing context.
