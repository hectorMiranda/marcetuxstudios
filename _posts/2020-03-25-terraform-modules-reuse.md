---
layout: post
title: "Terraform modules and what makes them actually reusable"
date: 2020-03-25
author: marcetux
tags: [terraform, iac, azure, devops]
---
We have four environments in Azure and the Terraform for each started as a copy-paste
of the last one. That's fine for the first two environments and embarrassing by the
fourth — three-week-old fixes haven't made it to prod because someone forgot which
directories to update. Modules are the answer, and March's remote-work homework was
finally extracting them.

A Terraform module is just a directory with its own variables, resources, and outputs.
The consumer passes in the variables; the module is responsible for the internal
wiring. The discipline that makes them reusable: inputs should be typed and documented,
outputs should expose what callers actually need, and the module should have a sensible
default for everything optional. A module that requires twenty inputs to deploy a
single App Service isn't reusable — it's copy-paste with extra steps.

The ones we extracted this month — `app-service-with-keyvault`, `aks-cluster`,
`apim-instance` — are now the single source of truth for those patterns. Prod, staging,
dev all call the same module with different variable files. A change to the App
Service pattern lands everywhere in one PR. The duplication wasn't free; I just
wasn't paying the bill every day.
