---
layout: post
title: "Terraform for small consulting projects"
date: 2024-10-08
author: marcetux
tags: [terraform, iac, infrastructure, consulting, devops]
---
I've been using Terraform for consulting infrastructure since 2020 and I want to
write down the pattern I've converged on for small projects — one to five engineers,
no platform team, AWS or GCP. The common mistake at this scale is either skipping
IaC entirely ("we'll add it later") or applying enterprise Terraform patterns that
require three people to understand the module structure. Neither serves a small project.

The pattern: a single Terraform workspace per environment (dev, prod), flat module
structure (no nested modules until the plain variable becomes genuinely limiting),
remote state in an S3 bucket or GCS bucket from day one. The remote state decision
is the only non-negotiable — local state files in a developer's checkout are a
disaster waiting to happen when that developer is unavailable and someone else
needs to make an infrastructure change. Two hours setting up remote state on day one
saves a crisis later.

The discipline I've added this year: the Terraform workspace gets a `README.md` that
explains what each resource group is for, the naming convention, and how to add a new
resource to an existing resource group rather than creating a new one. This sounds
like documentation overhead. In practice it's the difference between a handoff that
takes a day and one that takes a week. Infrastructure code is read by people who
didn't write it under pressure. Make it legible. The resource names are not
documentation; the README is.
