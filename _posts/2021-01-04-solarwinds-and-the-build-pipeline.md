---
layout: post
title: "SolarWinds and the build pipeline"
date: 2021-01-04
author: marcetux
tags: [security, supply-chain, devops, architecture]
---
The SolarWinds story kept circling in my head over the holidays. Not the political
noise — the engineering part: someone slipped malicious code into a build artifact
that tens of thousands of organizations downloaded and trusted. The attack surface
wasn't the product's runtime; it was the pipeline that built the product. That's a
different problem than the ones most security frameworks are written to solve.

At the bank, our build pipelines are Azure DevOps with a set of trusted feeds and
a Sonar gate. The SolarWinds case is a reminder that "trusted feed" is itself an
assumption that deserves scrutiny. If the build agent pulls a package from a mirror
and that mirror was compromised upstream, the Sonar gate won't save you — the
artifact looks fine. The thing that would have helped is a provenance check: can
you trace exactly what went into the binary and verify it matches what you think
you approved? NuGet lock files, pinned hashes, signed packages — the boring hygiene
the team never quite finds time for.

I spent a Monday morning after the break reviewing what we actually pin versus what
we just "upgrade when there's a problem." The answer was uncomfortable. First fix
is a proper lock file committed alongside the solution and enforced in the pipeline.
It doesn't prevent every scenario SolarWinds demonstrated, but it at least means a
surprise version bump is loud instead of silent.

*Update, December 2021: Log4Shell arrived and the SBOM argument from January
became urgent rather than theoretical. A software bill of materials is what lets
you answer "do we have this?" in minutes instead of in a 3 a.m. sprint through
grep and Maven dependency trees. Every prediction from the SolarWinds analysis
applies directly. The tooling for SBOM generation (CycloneDX, SPDX) has improved
throughout the year; the window to adopt it before the next Log4Shell is still
open. Barely.*
