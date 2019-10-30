---
layout: post
title: "Policy as code in Azure and the enforcement conversation"
date: 2019-10-29
author: marcetux
tags: [azure, compliance, policy, banking, devops]
---
Azure Policy lets you define rules that Azure enforces across the subscription — deny creation of resources without specific tags, audit resources missing encryption at rest, deny VM sizes outside the approved list. We've been using it in audit mode since summer and switched the most important policies to enforce mode last month. The conversation that switching required was the one worth writing about.

Audit mode collects violations; it doesn't prevent them. You see the drift, file tickets, and fix things. Enforce mode prevents non-compliant resources from being created. The resistance to switching came from two places: teams that had been ignoring the audit findings (because audit had no consequence) and genuine concern about breaking emergency processes — "what if I need to spin up a VM quickly during an incident and the policy rejects it." The first concern is the reason to switch. The second is a legitimate operational concern.

The answer to the second concern: the emergency process gets a documented exception procedure, not a policy loophole. An on-call engineer who needs to create a non-compliant resource in an emergency can request a short-term policy exemption through an automated runbook — it creates the exemption scoped to their principal for 4 hours and logs it in the audit trail. The exception exists; it's just explicit and auditable. The argument "we shouldn't enforce because we might need to break the rule" is almost always better answered by "we should enforce and make breaking the rule visible and intentional."
