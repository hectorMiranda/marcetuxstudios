---
layout: post
title: "Architecture decisions at small-team scale"
date: 2017-10-19
author: marcetux
tags: [architecture, startup, leadership, dotnet]
---
The most useful reframe I've had in the first month as CTO: architecture decisions have
to be evaluated against the team that will implement and maintain them, not the team you
might have in 18 months. A microservices architecture makes sense when you have teams
that need to deploy independently and own separate domains. We have two engineers. The
overhead of microservice coordination — separate deploys, separate schemas, distributed
tracing, inter-service auth — is overhead that doesn't buy us anything a well-structured
monolith doesn't.

The Go RN backend is a .NET Core monolith with clear internal module boundaries. The
`Facilities` module doesn't reach into the `Billing` module's database tables; they
communicate through service interfaces. The `Shifts` module owns its schema. The
boundaries are enforced by convention and code review, not by separate deployment
units. When the team grows to the point where the boundaries become cross-team
coordination bottlenecks, we extract — and by then the domain is well-understood
because we've been living in it.

The phrase I keep coming back to: match your architecture to your team. Conway's Law
isn't a suggestion. If your system has six independent services and your team has two
engineers, your two engineers spend half their time on coordination that wouldn't exist
in a monolith. That's not a technology choice; it's a staffing tax. Get the boundaries
right first; get the deployment model right when the boundaries are under team-level
pressure.
