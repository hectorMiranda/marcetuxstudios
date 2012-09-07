---
layout: post
title: "Goodbye Subversion, hello Git"
date: 2012-09-06
author: marcetux
tags: [git, svn, workflow]
---

We finally moved a project off Subversion this week, and the thing that sold the
team wasn't the distributed-ness — it was cheap branching.

In SVN a branch is a server-side copy and a small act of courage. In Git a branch
is a 40-byte pointer, so you make one for every little thing and throw it away
when you're done. That single change rewires how you work: experiments stop being
scary.

The migration itself was undramatic. `git svn clone` to pull the history, a pass
to map authors, then a fresh bare repo everyone re-cloned from. The harder part
was social — convincing people that `commit` and `push` are two separate ideas
now, and that a local commit isn't "saving to the server."

My advice if you're about to do this: don't try to preserve every SVN branch.
Bring trunk and the active branches, archive the rest as tags, and move on. Nobody
is going to `git checkout` that 2009 release branch.
