---
layout: post
title: "Azure DevOps pipeline templates for team reuse"
date: 2020-09-16
author: marcetux
tags: [devops, azure, cicd, teamwork]
---
We run both Azure DevOps pipelines and GitHub Actions at the bank — legacy services in
ADO, greenfield on GitHub. The ADO pipelines grew organically and by August we had six
pipelines that were 80% identical with six slightly different copies of the same build,
test, and security-scan steps. Updating the scan tool version meant touching six files.

ADO pipeline templates work like Helm chart templates: you extract the common steps
into a template yaml file in a shared repository, and each pipeline extends it with
`template:` references. The template handles the invariant parts — restore, build,
run tests, run Trivy, push to registry — and exposes parameters for the things that
vary: the service name, the registry path, whether to run integration tests.

The migration was a two-day project that's paid for itself twice already. When we
updated the Trivy version for a newly-detected vulnerability pattern, it was one file
in the templates repo, one pipeline run to verify, and all six services picked it up
on their next run. The discipline: if you're copy-pasting pipeline YAML, stop. The
third copy is the moment to extract a template. The second copy is already the
warning.
