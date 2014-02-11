---
layout: post
title: "Vagrant for reproducible dev environments"
date: 2014-02-10
author: marcetux
tags: [vagrant, devops, virtualbox, workflow, linux]
---
The version of "works on my machine" that kept happening was the SQLServer version.
Two developers on the team had different patch levels, one of them would update a
stored procedure, and three days later the other would hit a stored proc that behaved
differently on their box. Not subtle differences — actually different behavior on an
edge case in the aggregation. Vagrant was the fix we should have done six months ago.

A `Vagrantfile` is a few dozen lines of Ruby-ish config that tells Vagrant what VM
image to start, what ports to forward, and what provision script to run on first boot.
The provision script installs dependencies and configures the dev environment: SQL Server
Express, the right version, the right collation, the schema migrations applied from
scratch. Now the onboarding story is `vagrant up`, not a two-page wiki document that
drifted out of date nine months ago.

The tradeoff is memory and startup time. The VM eats RAM, and `vagrant up` from a cold
state takes a few minutes. But that cost pays off fast: a fresh developer gets a working
environment in one command, and "it works here but not in CI" almost never happens
anymore because CI and dev share the same base image. Reproducible is worth the overhead.
