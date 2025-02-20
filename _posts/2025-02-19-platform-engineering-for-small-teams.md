---
layout: post
title: "Platform engineering doesn't require a platform team"
date: 2025-02-19
author: marcetux
tags: [platform, devops, consulting, architecture, process]
---
Every third client conversation this month started with some version of "we need a
platform team." What they actually need is the outcome platform teams produce — a
smooth path from code to running software — and in most cases they can get 80% of it
without headcount dedicated to internal tooling. The distinction matters because waiting
for a platform team to hire is a real delay, and borrowing the pattern without the org
chart isn't.

The practical version for a small engineering org: standardize on one cloud provider,
one container platform, one deployment target, and codify that choice in a
gold-standard repository template that new services start from. The template includes
CI pipeline, Dockerfile, infrastructure-as-code for the standard runtime, and a
runbook. Spending a week on the template saves every subsequent service from
reinventing the same five decisions. That's the platform engineering outcome, delivered
by whoever cares most about it, probably a senior eng, not a team of three.

The honest caveat: this stops working at scale. When you have 80 services and 150
engineers the coordination costs justify dedicated platform work. But for the five- to
twenty-engineer teams I mostly advise, the gold-template approach is the right level of
rigor. Match the organization to the org. The pattern before the headcount.
