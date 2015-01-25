---
layout: post
title: "Capistrano deploys without surprises"
date: 2015-01-24
author: marcetux
tags: [ruby, capistrano, deployment, devops]
---
A Rails deploy that involved hand-copying config files and running a migration by SSH was
the state of things when I joined. Capistrano 3 fixed most of that in a week, and the
remaining uncertainty about "did the migration run?" disappeared completely.

Capistrano's model is tasks and roles: you declare that the web role runs the Puma
restart, the db role runs the migration, and the tool figures out the SSH fan-out. The
deploy flow is release directory per deployment, shared directory for uploads and the
config files that shouldn't be in the repo, and a `current` symlink swapped atomically
at the end. A failed deploy leaves the previous release intact; you roll back by
pointing `current` back. That's the whole model, and it's the right one — the deploy is
not destructive.

The part that made the biggest difference was the migration task. Capistrano runs
`db:migrate` on exactly one db-role server after the code is uploaded, before the symlink
flips. So by the time Puma restarts and starts serving requests, the schema is already
current. The old way — deploy, then SSH in and run the migration, then restart — had a
window where new code ran against an old schema. That window is gone. Deploy boring,
rollback fast.
