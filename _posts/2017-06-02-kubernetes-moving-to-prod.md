---
layout: post
title: "Moving the first service to Kubernetes in production"
date: 2017-06-02
author: marcetux
tags: [kubernetes, aws, devops, architecture]
---
After three months of running Kubernetes on the home cluster and the small AWS test
environment, I made the case to move the first production service to it: the feed
transformation Node service from February. The argument was straightforward —
stateless, containerized, already separated from the .NET core by the RabbitMQ boundary.
If it fails to deploy, the worst outcome is delayed feed processing, not a transaction
rollback.

The production setup is EKS-adjacent — we're using kops to manage a Kubernetes cluster
on EC2 rather than waiting for EKS to GA. The migration plan: Deployment manifest,
Service manifest, ConfigMap for environment config, a rolling update strategy with
`maxUnavailable: 0` so the old pods stay alive until the new ones pass their readiness
probe. The readiness probe itself is a lightweight `/health` endpoint in Express that
checks the RabbitMQ connection and returns 200 if it's up. Kubernetes won't route
traffic to a pod until that probe passes.

The first deploy went cleanly. The thing I was wrong about: I assumed the hardest part
was the Kubernetes config. The hardest part was writing the health endpoint in a way
that actually reflected service health rather than "is the process alive." A process
can be alive and completely unable to do its job — disconnected from the broker, out
of memory, stuck on an unprocessable message. The readiness probe has to ask the right
question. Now it does.
