---
layout: post
title: "Platform engineering is not DevOps renamed"
date: 2024-03-06
author: marcetux
tags: [platform-engineering, devops, kubernetes, infrastructure, architecture]
---
I keep hearing "platform engineering" used as a synonym for DevOps with a new label,
and I want to draw the line more carefully because the confusion leads to bad
organizational decisions. DevOps is a practice — a set of cultural and tooling norms
around how development and operations collaborate. Platform engineering is a product
discipline: you are building an internal product that other engineers are your users.

The distinction matters because the success metrics are different. A DevOps practice
succeeds when the org ships faster and operates more reliably. An internal platform
succeeds when its users — the application engineers — can do their work without
needing to become Kubernetes experts. The platform team absorbs the complexity so
the feature teams don't have to. If your "platform engineering team" is just
maintaining Helm charts that feature teams still have to understand, you've renamed
the team and changed nothing.

The shift I've seen work: the platform team treats developer experience the same way
a product team treats customer experience. User research (what's slowing you down?),
dogfooding, a public roadmap, and metrics on adoption and time-to-first-deploy.
Kubernetes is not the deliverable — the experience of deploying an app without
knowing Kubernetes exists is. That inversion of focus is what separates a real
platform from a well-documented infrastructure team.
