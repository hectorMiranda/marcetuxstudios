---
layout: post
title: "Azure DevOps Environments and deployment tracking"
date: 2019-11-20
author: marcetux
tags: [azure, devops, deployment, pipelines, architecture]
---
Azure DevOps added Environments earlier this year and I've been late to adopt them. The feature is a layer above pipeline stages: you define named environments (dev, staging, production) and the pipeline deploys to them with tracked history — who deployed what version when, with approval gates configurable per environment. I finally wired them up properly this month and the compliance team's question "what is in production and when did it get there" became answerable from a single screen.

The environment deployment history shows each deployment run, the pipeline that triggered it, the git commit deployed, and the approvals recorded. For a bank where change management records are a regulatory requirement, this replaces a manual change ticket with an automated audit trail. The compliance team can see that deployment X to production happened at a specific time, was approved by two named individuals, and the commit it deployed is the one that passed the test gate. The pipeline is the change record.

The approval gate in the production environment requires two sign-offs: the tech lead and a member of the compliance engineering team for compliance-sensitive services. The approval request goes out through the pipeline notification; the approver sees the change description from the PR, the test results, and the staging deployment confirmation. They have everything they need to make an informed decision without a separate change management meeting. The process didn't get slower; it got more documented. That's the trade worth making.
