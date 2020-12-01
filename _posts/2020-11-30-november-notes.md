---
layout: post
title: "November notes"
date: 2020-11-30
author: marcetux
tags: [meta, retrospective]
---
November was a milestone month. .NET 5 is GA, used in production, and living up to
the preview. The M1 benchmarks are real and the ARM64 story for the platform is
clearer than it's been at any point this year. C# 9 records have been in production
for three months and the category of "accidental mutation" bugs has gone quiet. These
are the kinds of changes that compound across the next few years.

Blazor WebAssembly production lessons are in the blog now. The short answer: it
works, the first-load concern is real but manageable, and the developer experience
is genuinely better than anything else I've shipped for internal C# teams.

The reusable GitHub Actions workflow is the unglamorous infrastructure piece that
will matter most next year. Standardizing the CI pipeline means the security and
compliance controls are in one place, enforced consistently, and auditable. December
will be light — end of year reviews, planning — but I want to write about what the
full-remote year actually changed about how I architect distributed systems, while
it's still fresh.
