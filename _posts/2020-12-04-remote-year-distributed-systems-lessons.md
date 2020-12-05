---
layout: post
title: "What a year of remote work taught me about distributed systems design"
date: 2020-12-04
author: marcetux
tags: [architecture, remote, process, distributed]
---
Nine months remote has clarified something about distributed systems design that
offices obscure: the properties that make a system resilient and observable are the
same properties that make a *team* resilient and observable when working apart.
I don't think this is coincidence.

Async by default. A distributed system that requires synchronous coordination — a
deploy that requires two people online simultaneously, an API that requires a
downstream to be available — is fragile at the seams. A team that requires in-person
coordination — a decision that only gets made in a meeting, a handoff that only
happens in person — is the same fragility at a human scale. The runbooks, the ADRs,
the async incident process we built this year aren't just remote-work adaptations;
they're the written state that a distributed system needs to function without a shared
physical context.

Observability before optimization. We spent the first six months of remote work
building observability — Splunk searches, correlation IDs, Prometheus on the cluster —
and it paid off in the second six months when we were debugging incidents without
being in the same room. A system you can't see is one you can't fix remotely. A team
whose work is invisible fails for the same reason. The dashboard you build for the
system, the status update you write during an incident, the ADR you file after the
decision — all of it is the same answer to the same problem.
