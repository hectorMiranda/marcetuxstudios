---
layout: post
title: "Helm charts as repeatable deployment definitions"
date: 2019-07-09
author: marcetux
tags: [kubernetes, helm, devops, azure, deployment]
---
We had three environments — dev, staging, production — each with slightly different deployments. The differences were in CPU/memory requests, replica counts, and which Key Vault each environment pointed to. The deployments were maintained as three sets of YAML manifests with the shared parts copy-pasted and the differences scattered. Every change to a shared part required touching three files and the inevitable divergence had already bitten us once: a staging manifest had a newer health check path than production, and staging was testing something production didn't have.

Helm solves this with templates and values files. One chart — one set of templates describing the Deployment, Service, HPA, and network policies — and three values files (`dev.yaml`, `staging.yaml`, `production.yaml`) that declare the differences. The pipeline renders the chart with the appropriate values file and applies the result. A change to the health check path goes in one place; all three environments pick it up on the next deploy.

The discipline that makes Helm usable: keep the chart simple. When I see a Helm chart with thirty conditional blocks and `if/with/range` logic covering every possible configuration axis, the chart has become harder to read than raw YAML. Ours have three values files and the templates have maybe two or three conditionals. If an environment needs something genuinely different in structure — not just a different value but a different resource — that's a signal to question whether it should be the same service. Helm for configuration variance; different charts for structural differences.
