---
layout: post
title: "Azure DevOps variable groups and pipeline secrets"
date: 2019-06-25
author: marcetux
tags: [azure, devops, ci, secrets, pipelines]
---
We locked down the Key Vault reference pattern for runtime configuration in February. The gap that remained was build-time secrets — the credentials the pipeline needs to push images to ACR, deploy to AKS, and run integration test databases. Those had been living as plain-text variables in the classic release pipeline definitions, which is not where secrets should live.

Azure DevOps variable groups backed by Key Vault are the answer for pipeline secrets. You create a variable group, link it to a Key Vault, and pull in specific secrets by name. The pipeline references the variable group and the variables are injected as environment variables at runtime — masked in logs, never stored in the pipeline definition itself. When the Key Vault secret rotates, the pipeline picks up the new value on next run without any change to the pipeline YAML.

The remaining friction is the bootstrap secret: the service principal that the pipeline uses to authenticate to Key Vault itself needs its credentials stored somewhere. That one lives as a protected pipeline secret directly in Azure DevOps, set by hand, rotated on a calendar, and governed by service principal policies with a 90-day expiry that forces the rotation. There is no secret-free bootstrap — you have to start with some credential. The discipline is knowing which one it is, keeping it minimal in scope (read-only on Key Vault, nothing else), and rotating it on schedule. Everything else cascades from the Key Vault, and that one credential is the root you guard.
