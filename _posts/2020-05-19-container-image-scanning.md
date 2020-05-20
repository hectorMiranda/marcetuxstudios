---
layout: post
title: "Container image scanning in the build pipeline"
date: 2020-05-19
author: marcetux
tags: [docker, security, devops, cicd]
---
Dependabot covers NuGet and npm packages; it doesn't cover the OS packages baked
into a container base image. A `mcr.microsoft.com/dotnet/aspnet:3.1` base from six
months ago has CVEs the base image maintainers have since patched — you just never
rebuilt. The container layer is a blind spot that package scanning doesn't see.

Trivy is the tool I've integrated into the GitHub Actions pipeline. It scans an image
after build, classifies findings by severity, and exits non-zero on anything high or
critical. The job fails; the image doesn't get pushed. For the bank's pipelines this
is the right gate — a failed scan is a conversation with the developer today rather
than a finding in the next security audit. The scan takes about thirty seconds on a
typical .NET image, which is acceptable pipeline overhead.

The useful operational change is using the most specific base image tag available —
`3.1.18` not `3.1` — and rebuilding base images weekly even when application code
hasn't changed. The weekly rebuild is a GitHub Actions scheduled workflow: pull the
base, rebuild the service images, scan, push if clean. Base-image CVEs now have a
maximum exposure window of one week instead of "whenever we happened to rebuild."
That's not perfect, but it's a defined, bounded risk rather than an undefined one.
