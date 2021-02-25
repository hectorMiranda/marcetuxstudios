---
layout: post
title: "Which pipeline gates are actually worth keeping"
date: 2021-02-24
author: marcetux
tags: [devops, azure-devops, ci, quality]
---
The CI pipeline had eighteen stages by the time someone sent me a screenshot and
asked why builds took forty minutes. Counting backward through the audit log: most
of the stages had been added after incidents, each one a promise to "prevent this
from happening again." The pipeline grew without anyone removing anything. It's a
common kind of technical debt — invisible, load-bearing in appearance, actually
just weight.

The test I applied to each gate: does this catch something the earlier gates missed,
and how often does it catch it? A gate that never triggers in six months is either
perfect code (unlikely) or a broken gate that's costing build time for nothing.
We killed five stages in the first pass — two integration test suites that hadn't
caught a regression in a year, a dependency-vulnerability scan that was overridden
by policy every time it fired, and a pair of performance benchmarks against a
decommissioned environment. Forty minutes dropped to eighteen.

The discipline is treating the pipeline the same way you treat production code: it
needs to be maintained, not just added to. Every gate has a cost in build time and
in the attention it demands when it fails. A gate that cries wolf trains the team
to click through. Fewer, meaner gates that actually fail on real problems are worth
ten theater stages that pass unconditionally. Keep what bites. Remove what doesn't.
