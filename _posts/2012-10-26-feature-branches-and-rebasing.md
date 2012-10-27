---
layout: post
title: "Feature branches and the rebase habit"
date: 2012-10-26
author: marcetux
tags: [git, workflow, version-control]
---

A month into Git and the team has settled into a rhythm worth writing down, because
it's mostly about *history as a document*, not just storage.

Every change starts on a branch off `master`. Small, focused, named for the thing
it does. The branch is cheap (the whole reason we switched), so nobody batches
three unrelated changes into one anymore.

The habit that took convincing was **rebasing before merge**. When `master` has
moved on, I `git rebase master` on my branch so my commits replay on top of the
current tip. The payoff is a history that reads as a straight, intentional story
instead of a thicket of merge bubbles. Conflicts surface on my branch, where
they're my problem to solve, not in a giant merge commit nobody understands later.

The one firm rule: **never rebase anything you've pushed and shared.** Rewriting
history under someone else's feet is how you make enemies. Rebase your *local*
work to tidy it; once it's public, it's public.

`git log` should read like a changelog. Rebasing is how you keep it that way.
