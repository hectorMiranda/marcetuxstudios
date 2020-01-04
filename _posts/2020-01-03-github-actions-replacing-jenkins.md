---
layout: post
title: "GitHub Actions and the case against Jenkins"
date: 2020-01-03
author: marcetux
tags: [devops, cicd, github, automation]
---
The bank's CI story has been Jenkins since before I got there, and Jenkins is fine
right up until you want to do anything that isn't on its happy path. I spent two
days in December trying to add a parallel test stage and ended up deep in a
Groovy shared-library rabbit hole. New year felt like the right moment to try
something different on a greenfield pipeline.

GitHub Actions is the thing I kept reading about through the second half of 2019.
The model is yaml workflow files that live in the repo next to the code, triggered
by events — push, pull-request, scheduled — and assembled from composable actions.
What clicks is that the workflow *is* the documentation: no external Jenkins config
to drift from what the code actually builds; the file is there in history with every
other change. Secrets management, caching, matrix builds — all in the same file.

I wired up a .NET Core build: restore, build, test, and artifact upload, all
running on a Microsoft-hosted runner. Twenty minutes from first yaml to green
pipeline on a real branch. Jenkins can orchestrate things Actions can't touch yet —
it's more flexible when you own every node — but for the majority of pipelines where
I need build-test-publish and nothing exotic, Actions is now my default answer.
Fewer moving parts, closer to the code, a tired colleague can't misconfigure it.
