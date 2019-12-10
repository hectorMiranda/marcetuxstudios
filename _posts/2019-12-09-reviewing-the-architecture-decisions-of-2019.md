---
layout: post
title: "Reviewing the architecture decisions of 2019"
date: 2019-12-09
author: marcetux
tags: [architecture, retrospective, banking, process]
---
December is the time I go back and honestly assess the decisions I made, not the ones that felt good at the time. Twelve months at City National Bank, first full year as the integration architect. Some things I'm proud of; a few I'd do differently.

The decisions that worked: OpenAPI contract-first changed how teams integrate with each other in a way that's still compounding. The mTLS rollout hit every target and the security team is now asking for it on services I didn't plan for yet. Structured logging and correlation IDs reduced mean-time-to-understand by something I can't measure precisely but can feel clearly in the difference between Q1 and Q4 incident response. The service template enforcing good defaults has lowered the compliance review burden measurably.

What I'd do differently: gRPC adoption moved more slowly than the technical case warranted, because I didn't bring the ops team along early enough. They were asked to support a technology they'd never monitored in a regulated environment with limited tooling. I should have run a joint ops/dev spike on observability and runbook before the first production service. The technology was right; the readiness of the organization around it wasn't, and that's an architect's job to assess. The year's theme in retrospect: the right architecture is the one your organization can actually operate, not the optimal one for the problem in isolation.
