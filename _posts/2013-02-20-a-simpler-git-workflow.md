---
layout: post
title: "A simpler git workflow"
date: 2013-02-20
author: marcetux
tags: [git, workflow, devops, process]
---
I adopted git-flow last year because it was the documented Right Way, and I've spent
2013 quietly abandoning it. With continuous deployment, the elaborate
develop/release/hotfix branch dance solves problems I no longer have, and creates
ceremony I do.

git-flow assumes you cut releases — that "what's in production" and "what's done" are
different things separated by a release branch. But if every green build can ship,
`master` *is* production, and a long-lived `develop` branch is just a second place for
things to drift out of sync. The release and hotfix branches are machinery for a
cadence I don't run anymore.

So I've moved to the simpler thing everyone's calling GitHub flow: `master` is always
deployable, you branch off it for any piece of work, you open a pull request to
discuss and review, and you merge back to `master` when it's green — and that merge is
a deploy. Feature flags, from January, are what make this safe for unfinished work:
the branch stays short-lived because the half-done feature ships dark behind a flag
instead of living on a branch for three weeks. Match the workflow to the cadence. Mine
got faster, so the workflow got smaller.
