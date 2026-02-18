---
layout: post
title: "Azure DevOps pipeline structure that scales across services"
date: 2026-02-17
author: marcetux
tags: [azure-devops, cicd, devops, dotnet, pipelines]
---
When the team had two services, the Azure DevOps pipelines were each a single YAML file and nobody thought much about it. Now there are eight, and the pattern of "one bespoke pipeline per service" has consequences: a change to the build standard — update the .NET SDK version, add a SAST scan step, change the artifact naming convention — means eight pull requests to eight different files, and the probability that they all land consistently before someone's Friday afternoon deploy is low.

The solution is the template pattern that Azure DevOps supports but doesn't force you to use. Common stages — restore, build, test, publish, deploy-to-staging, deploy-to-production — live in shared template files in a `pipelines/templates/` repository. Each service's pipeline file is small: it declares parameters (service name, project path, whether the deploy is gated by an approval), calls the templates, and does nothing else. The templates own the standard; the service files own the configuration.

The friction I expected — template changes breaking services that didn't ask for the change — is real but manageable. Semver the templates: a compatible improvement bumps the minor version, a breaking change bumps the major, and services pin to a major version explicitly. Services update to a new major deliberately, not because a refactor landed upstream. That's the discipline that makes shared infrastructure actually shareable rather than a shared source of surprise.
