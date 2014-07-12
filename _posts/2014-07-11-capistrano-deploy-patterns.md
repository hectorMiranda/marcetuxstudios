---
layout: post
title: "Capistrano deployment patterns that hold up"
date: 2014-07-11
author: marcetux
tags: [ruby, capistrano, devops, deployment, rails]
---
The deployment pipeline at Spark runs through Capistrano, which was new to me. The
basic model is SSH into the servers, run a sequence of commands in order: fetch the new
code, run migrations, restart the application server, symlink the release. That sequence
is enough to ship Rails; the patterns that make it safe are the additions on top.

The `linked_files` and `linked_dirs` configuration is the one I learned first. Config
files that differ between environments — `database.yml`, `.env`, S3 credentials — live
in a `shared` directory on the server and get symlinked into each release. They never
live in the repository. New developers sometimes miss this and wonder why their database
config disappears on deploy; the answer is that the deployed config was always the shared
one, and the local copy is irrelevant.

Zero-downtime deploys are the second pattern. The default Capistrano sequence restarts
Unicorn completely, which drops connections during the restart window. Phased restarts —
stopping one worker at a time while the rest serve requests — keep the app available
throughout. The `kill -s USR2 <unicorn_pid>` signal tells Unicorn to spawn a new master
with the new code and gracefully wind down the old workers. It's not instant but it's
continuous, and for a consumer app the difference between "site down for three seconds"
and "some requests took longer than usual" is the difference between a visible incident
and background noise.
