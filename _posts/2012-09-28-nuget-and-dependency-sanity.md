---
layout: post
title: "NuGet and a little dependency sanity"
date: 2012-09-28
author: marcetux
tags: [dotnet, nuget, dependencies, tooling]
---

Cleaned up a project's dependencies this week and it reminded me how much NuGet has
quietly changed .NET work. I still remember committing a `lib/` folder full of DLLs
to source control and praying everyone had the same versions.

Now `packages.config` lists what we depend on and the versions, NuGet restores them
on build, and the binaries don't live in the repo. The diff when someone bumps a
package is one readable line instead of a binary blob.

Two habits I'm enforcing on the team:

- **Pin versions.** Let a transitive dependency float and one day a clean restore
  pulls something new and the build breaks for reasons nobody changed on purpose.
- **Enable package restore, don't commit the binaries.** The repo should say what
  you need, not carry it.

The thing I'm still wary of is dependency sprawl — pulling a whole package for one
helper method you could have written in ten lines. Easy in, hard to remove later.
Convenience has a carrying cost, and somebody pays it at upgrade time.
