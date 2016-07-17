---
layout: post
title: "Immutable deployments and why I stopped patching instances"
date: 2016-07-16
author: marcetux
tags: [devops, deployment, aws, docker, infrastructure]
---
For a long time our deployment model was: SSH to the instance, pull the new Docker
image, restart the container. It worked until it didn't — a failed mid-deploy restart
that left the container in a bad state, a `docker pull` that timed out leaving the old
image running, a config change that didn't propagate to all instances. Patching running
instances is a process that fails in a hundred small ways and you only notice some of
them.

The immutable approach eliminates the category: you never patch a running instance.
You build a new image, push it to ECR, update the Auto Scaling launch configuration
to reference the new image tag, and trigger a rolling replacement. New instances launch
with the new image; old instances drain their current job and terminate. The new
state is deployed by replacing, not by updating. The running state you trust is the
one that came off the image, not the one that accumulated patches over six months.

What this requires: your instances must be truly stateless — no data on the instance
that isn't in S3 or a database, no configuration that isn't in environment variables or
a config service. We'd already done the stateless work for the SQS scaling; the
immutable deployment model just extends that discipline to the infrastructure. The
constraint is the feature. You can't accidentally have state on an instance if an
immutable deployment destroys the instance.
