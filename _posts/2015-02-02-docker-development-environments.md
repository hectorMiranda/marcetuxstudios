---
layout: post
title: "Docker for consistent dev environments"
date: 2015-02-02
author: marcetux
tags: [docker, devops, ruby, tooling]
---
The classic new-dev onboarding problem: the Rails app works on my laptop and not on the
next person's because of a Ruby version mismatch, a missing library, or a Postgres
config difference I forgot I'd made months ago. We've been using rbenv and a shared
wiki page of setup steps, which is better than nothing and worse than a `Dockerfile`.

Docker 1.4 is stable enough to use for this without feeling reckless. I wrote a
`Dockerfile` for the Rails app — base image, bundle install, environment vars — and a
`docker-compose.yml` that links in a Postgres and a Redis container. The developer
runs `docker-compose up` and gets the full stack in the same state as everyone else,
regardless of what's on their host. The first time through, it pulls images, which is
slow. Every time after that, layers are cached and it's fast.

The friction is volume mounting for development: you want your local source changes
reflected instantly without rebuilding the image each time. Docker's volume mount covers
this, though there are permission edge cases on Mac where the mounted paths end up owned
by root inside the container and the Rails process can't write tmp files. Solvable, just
annoying. The broader point stands: describing the environment in code instead of a wiki
page means the environment is the spec, not the documentation. If the container works, it
works everywhere.
