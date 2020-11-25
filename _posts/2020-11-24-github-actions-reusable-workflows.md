---
layout: post
title: "GitHub Actions reusable workflows and team standardization"
date: 2020-11-24
author: marcetux
tags: [devops, github, cicd, teamwork]
---
The GitHub Actions ecosystem moved in November: reusable workflows are in preview,
which is the Actions-native answer to the shared-library problem I solved in ADO with
pipeline templates. In Actions, a reusable workflow lives in its own repository, is
referenced with `uses: org/shared-workflows/.github/workflows/dotnet-build.yml@main`,
and accepts inputs just like a callable function.

The ADO template approach has been working well, but the bank has moved more new
services to GitHub, and maintaining two shared-pipeline systems is a maintenance
cost with no payoff. The plan: extract the common .NET build steps into reusable
GitHub Actions workflows in a shared repository, converge new services there, and
let the ADO templates support legacy services without adding to them.

The immediate benefit is the same as the ADO template: one change to the security
scan version propagates to all callers without touching each service's workflow file.
The additional benefit specific to GitHub: the reusable workflow can enforce things
the calling workflow can't override — required permissions, required environments —
which is a compliance control baked into the CI system. The calling repo can't skip
the security scan because the security scan is inside the reusable workflow that
they can't edit. That's a meaningful audit artifact.
