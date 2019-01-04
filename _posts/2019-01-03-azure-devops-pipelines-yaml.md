---
layout: post
title: "Azure DevOps pipelines in YAML"
date: 2019-01-03
author: marcetux
tags: [azure, devops, ci, yaml, pipelines]
---
The bank had been on the classic "visual designer" Azure DevOps build definitions — point and click, configuration stored somewhere in a database nobody fully understood. The first thing I pushed for in January was moving to YAML pipelines, and after a couple of evenings proving it out in a personal project I had a model worth showing the team.

The big shift is that the pipeline lives in the repo. It gets reviewed like code, it branches with feature branches, and its history is the same as the code's history. No more "who changed the build and when" as an unsearchable mystery. The YAML schema took some getting used to — `trigger`, `pool`, `stages`, `jobs`, `steps` — but the structure forces you to think about what the pipeline actually is rather than clicking through a wizard and hoping the defaults are sane.

The part I like most is multi-stage: one YAML file declares build, deploy-to-staging, and deploy-to-prod as sequential stages, with approval gates between the last two. The whole promotion story is in version control. Old way — separate release pipelines bolted onto build definitions — needed too many people to understand too many screens. This way a new team member reads the YAML and understands the whole delivery path. Boring at the boundary, finally.
