---
layout: post
title: "Docker at JibJab and the production question"
date: 2015-08-14
author: marcetux
tags: [docker, devops, deployment, jibjab]
---
JibJab uses Docker for local development — the same pattern I introduced at Spark, but
more mature. Every service has a `Dockerfile`, `docker-compose.yml` brings up the full
stack locally, and it works. The question the team is actively discussing is the same
one everyone is discussing in 2015: do we use Docker in production?

The arguments for: environment parity, immutable deployments, service isolation. The
arguments against at our scale: the orchestration story is immature. Docker Swarm exists
but is basic. Kubernetes is gaining attention but we don't have the infrastructure
expertise or the operational runbook. ECS (Amazon's container service) is an option but
the tooling is early and the deployment workflows are unfamiliar. The on-call team needs
to be able to diagnose a production incident at 2 AM; adding container orchestration to
the diagnostic stack is a risk to weigh carefully.

The pragmatic call for now: Docker in dev, EC2 Auto Scaling in production with Capistrano-
style deploys. It's not architecturally elegant but the team can operate it confidently.
The Docker-in-production conversation is happening again every quarter and the tooling
keeps improving. When Kubernetes has a managed offering that doesn't require a PhD to
operate, the answer will probably change. Until then, boring in production is the right
kind of boring.
