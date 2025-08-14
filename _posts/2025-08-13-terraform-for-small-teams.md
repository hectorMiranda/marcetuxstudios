---
layout: post
title: "Terraform for small teams without a DevOps engineer"
date: 2025-08-13
author: marcetux
tags: [terraform, iac, platform, devops, consulting]
---
The standard advice for IaC is "use Terraform for everything," which is correct in
principle and overwhelming in practice for a five-person team that has never written
HCL and doesn't have anyone whose job is infrastructure. The advice I actually give:
Terraform for the infrastructure that changes rarely (network, IAM, databases), a thin
layer of automation for the infrastructure that changes often (deployments), and don't
fight the cloud console for one-off things you'll never need to reproduce.

The practical module structure that works: a `base` module for the persistent
infrastructure (VPC, RDS, IAM roles) that runs in a CD pipeline when a PR modifies it,
and a per-service module for the runtime environment (container definitions, load
balancer listener rules) that runs on every deploy. The base module has a slow cadence
and a long review process; the service module has a fast cadence and automated tests.
Same tool, different process per concern.

The state management piece is the one where teams consistently underinvest. Remote
state in an S3 bucket with DynamoDB locking isn't glamorous but it's the difference
between "anyone can run Terraform" and "whoever ran it last owns the state file in their
home directory." Set up remote state first, before the first `terraform apply`, not
after the first conflict. It's a ten-minute job that saves a painful hour later.
