---
layout: post
title: "First week at AmaWaterways"
date: 2026-01-04
author: marcetux
tags: [career, identity, dotnet, onboarding]
---
Starting a new job after a year of freelancing is a specific kind of culture shock. The freelance rhythm is all self-direction and async; a real team has meetings, Jira boards, a Confluence wiki that's seventy percent out of date, and a codebase that reflects every decision the previous five engineers made under pressure. Walking into AmaWaterways's identity platform on Monday felt like that — in the best way. There is actual work to do, and it matters, and someone is counting on me to not break it.

The stack is .NET 8 minimal APIs talking to Auth0, with Azure DevOps handling CI/CD. The team has clearly spent real effort here: the pipelines are sensible, the test coverage on the auth layer is better than I expected, and the OpenTelemetry instrumentation gives me enough signal to understand what's actually happening without grep-and-pray. The domain is river cruise operations — guests, agents, crew — and identity turns out to be genuinely interesting in that context. The guest lifecycle from inquiry to boarding is longer and more stateful than the typical e-commerce session, which is a good problem.

What I'm onboarding into, specifically, is their SCIM 2.0 provisioning layer. The concept is solid but the implementation has a few rough edges around idempotency and error handling that I'm already making notes on. First week I'm reading, not writing. I'll earn the right to have opinions by understanding what was built and why before I start moving things around.
