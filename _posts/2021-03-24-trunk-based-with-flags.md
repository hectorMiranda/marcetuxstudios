---
layout: post
title: "Trunk-based development that actually holds together"
date: 2021-03-24
author: marcetux
tags: [git, devops, feature-flags, process]
---
The team debated branching strategy again this quarter and I'm going to save future
me the trouble: the strategy doesn't matter nearly as much as whether you have a
working mechanism to decouple deploying code from releasing features. Every argument
for long-lived feature branches is really an argument against having that mechanism.

We landed on trunk-based with feature flags, which I've run in some form since 2013
and which remains the approach I trust most. Short-lived branches — a day or two —
off main, merged via PR after review, deployed immediately. Feature flags in a
central config (LaunchDarkly in this case) gate the new behavior so the code ships
but the feature is invisible until we turn it on. The branch is gone before the
feature is done. The flag is what's "in progress," not the branch.

The discipline this requires is flag hygiene. Every flag has an owner and an expiry
date. Flags that gate completed features get cleaned up within a sprint of release;
a flag left in the codebase for more than a quarter is technical debt the same
as any other dead code. The tooling is cheap; the discipline is not. But it's the
only way I've found to have both short-lived branches and the confidence that
half-done work isn't visible to customers.
