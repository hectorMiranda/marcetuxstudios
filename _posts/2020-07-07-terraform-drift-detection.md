---
layout: post
title: "Detecting infrastructure drift before it causes an incident"
date: 2020-07-07
author: marcetux
tags: [terraform, iac, azure, devops]
---
A firewall rule that a colleague added manually to an Azure Network Security Group
last month — a quick fix during an incident — was sitting there in production,
undocumented and unmanaged, while our Terraform said the rule didn't exist. Terraform
didn't fail; it just didn't know. The next time someone ran an apply that touched that
NSG, the rule vanished. That caused fifteen minutes of confusion before we traced it.

`terraform plan` in CI is the fix — not just on PR merge, but on a schedule. Run
`terraform plan` against prod every night, comparing current IaC with actual
infrastructure state. If the plan shows changes that nobody authored, something drifted.
The nightly output goes to a Teams channel; any plan that's not empty is worth
investigating before someone touches that resource. This isn't expensive — it's a
GitHub Actions scheduled workflow and a `terraform plan` against the remote state.

The cultural piece is harder: manual changes to production infrastructure need to be
treated like technical debt that gets Terraformed immediately, not someday. The
incident fix is the exception; the Terraform PR is the rule. "I'll add it later"
is where drift lives. We now track manually-made changes in the same Splunk search
that surfaces configuration events, which creates a paper trail and a reminder.
Automation doesn't prevent manual change; it makes the gap visible.
