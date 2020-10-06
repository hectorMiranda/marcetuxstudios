---
layout: post
title: "Practical .NET 5 migration guide for our services"
date: 2020-10-05
author: marcetux
tags: [dotnet, migration, architecture, csharp]
---
RC2 landed this week and the migration path from .NET Core 3.1 is clear enough to
write up. I've done it four times on internal projects by now, so the rough edges
are known. The short version: it's remarkably smooth if you're on 3.1 and less smooth
if you have any .NET Standard 2.0 class libraries that need to stay cross-compatible.

The mechanical steps: bump `TargetFramework` to `net5.0` in every project, update
all Microsoft.* packages to the 5.x versions, run `dotnet build` and fix the
breakage. Most of the breakage is package version mismatches or APIs that moved;
almost none of it is behavioral. The one gotcha to watch: `System.Threading.Tasks.Dataflow`
moved from a NuGet package into the BCL, so the explicit package reference breaks the
build. Remove the reference; it's included.

The bigger decision is whether to enable C# 9 features immediately or keep code style
consistent across the migration. My recommendation: use `<Nullable>enable</Nullable>`
immediately — the null reference type annotations have caught real bugs in every service
I've enabled them on — and introduce records and patterns incrementally over the
next few months as you touch code for other reasons. A migration commit should migrate;
C# 9 code can land in subsequent PRs. Keep the change surfaces separate so the
upgrade is reviewable.
