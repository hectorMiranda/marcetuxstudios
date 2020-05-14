---
layout: post
title: "GitHub Actions secrets and environment protection rules"
date: 2020-05-13
author: marcetux
tags: [devops, cicd, github, security]
---
As GitHub Actions replaced Jenkins on more bank pipelines, the secrets management
question got louder. Jenkins had a credentials store with roles; Actions has
repository and organization secrets, and the new environments feature that shipped
earlier this year adds required-reviewer gates before a job can access environment-
scoped secrets.

The model that's working for us: dev and staging secrets live at the repository
level, available to any branch. Production secrets live in a `production` environment
configured with a required-reviewer list — a merge to main triggers the deploy job,
but it can't access the prod secrets until a second engineer approves the pending
deployment. That approval gate is the same control we had in Jenkins but now it's
declared in the workflow yaml, visible in the repository, and auditable by the
same tooling we use for everything else.

The operational improvement over Jenkins: the secrets rotation workflow is the same
for every repo. Update the secret in the GitHub org settings or environment config,
it's live on the next run. No Jenkins credential store to synchronize separately, no
drift between what the Jenkinsfile references and what's actually configured. The
workflow file is the source of truth for how secrets flow, which means the security
team can audit it with a PR review instead of a Jenkins admin portal deep-dive.
