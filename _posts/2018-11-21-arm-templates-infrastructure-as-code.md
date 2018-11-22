---
layout: post
title: "ARM templates and the infrastructure-as-code discipline at a bank"
date: 2018-11-21
author: marcetux
tags: [azure, infrastructure, devops, enterprise, architecture]
---
The Service Bus namespace, the AKS cluster, the Key Vault, the App Service plan —
all of them started as point-and-click portal configurations during the initial setup.
All of them are now ARM templates in the team's infrastructure repository. The
conversion happened not because someone mandated it but because a namespace got
deleted by accident during a permissions review and the rebuild was slower than it
needed to be. That's the most reliable source of infrastructure-as-code discipline:
a recovery that takes longer than it should have.

ARM templates are not enjoyable to write. The JSON structure is verbose, the schema
documentation requires several browser tabs open simultaneously, and the error messages
are famously unhelpful — "Deployment failed" with a nested error chain that you
trace back through three levels of indirection to "the property 'retentionPeriodInDays'
is required." Terraform would be more ergonomic and I understand why teams prefer it,
but the bank's current cloud standards list ARM as the approved IaC tool and I'm not
going to fight that battle this quarter.

The practice worth establishing: a deployment pipeline that validates the ARM template
in CI (`az deployment group validate`) and runs a what-if against the target
environment before any apply. The what-if preview exists and it's useful — it tells
you what will be created, modified, or deleted before you commit. That's the check
that catches the cases where an innocent-looking template change would delete a storage
account.
