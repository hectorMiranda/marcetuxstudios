---
layout: post
title: "Rebase versus merge and when each belongs"
date: 2014-03-22
author: marcetux
tags: [git, workflow, process, devops]
---
A team argument this week about whether to rebase or merge feature branches surfaced
a real disagreement, not just a style preference. I've been using merge for months
because it's safe and explicit — the history shows every integration point. But the
history is also full of merge commits that add noise when I'm trying to read what
actually changed between two points in time.

Rebase reapplies the commits from a feature branch on top of the current tip of
main, producing a linear history without merge commits. The resulting history reads
like the feature was always built on the current codebase, which is clean for reading
but dishonest about what actually happened. The danger is rewriting commits that other
people have already pulled; rebasing shared history is how you wreck a teammate's
working copy.

The rule I settled on: rebase personal feature branches before opening a pull request,
so the reviewer sees a clean linear diff and the integration point is a single merge
commit with a clear message. Never rebase anything that's been pushed to main or shared.
The merge commit at the integration point is intentional — it marks where the feature
joined the mainline and why. History should show what happened, not be a fiction about
what you wish had happened.
