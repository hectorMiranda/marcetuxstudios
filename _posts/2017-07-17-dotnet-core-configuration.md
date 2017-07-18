---
layout: post
title: ".NET Core configuration and getting it right for multiple environments"
date: 2017-07-17
author: marcetux
tags: [dotnet, configuration, devops, architecture]
---
The .NET Core configuration system is one of the parts I've been most pleased with
moving from the Framework days. The old model — `web.config`, XML transforms per
environment, deployment-time token replacement — was fragile in a way that produced
configuration bugs that only surfaced in the wrong environment at the wrong time.
The new model composes environment sources in a stack, and the stack is code.

The setup I run: base values in `appsettings.json` (committed), environment-specific
overrides in `appsettings.{Environment}.json` (committed for staging/development
defaults, not for secrets), and actual secrets in environment variables that Kubernetes
injects from Secrets objects. The `IConfiguration` that the application sees is the
merged result — the environment variable overrides the JSON, the JSON provides the
fallback. No string replacement, no build-time tokens, no "which web.config transform
did this come from" archaeology.

The discipline: `appsettings.json` should only contain values that are safe to commit.
Connection strings with real credentials never go in the file; they go in environment
variables. A Kubernetes Secret holds the value, a `valueFrom.secretKeyRef` in the
Deployment manifest injects it as an environment variable, and the application reads
it from `IConfiguration` without caring how it arrived. The path from "secret stored
somewhere" to "application uses it" is short, auditable, and keeps the secret out of
git history.
