---
layout: post
title: "Docker in the build pipeline without making it the whole story"
date: 2016-02-04
author: marcetux
tags: [docker, devops, containers, pipeline]
---
Docker has been "the thing everyone should be using" for a while now and at JibJab
we've been using it inconsistently: some services containerized, some deployed the old
way. January was the month I finally got the transcoding workers building inside a
container end-to-end. The pitch for doing it here is simple — the worker needs FFMPEG,
libfdk-aac, and a specific set of codecs, and "install these six packages in this order"
is a conversation I've had with every new engineer and every new EC2 AMI.

A Dockerfile is that conversation written down. `FROM ubuntu:14.04`, layer in the
dependencies, copy the app code, define the entry point. The image is the artifact —
the same image that runs the CI tests runs in production, same codecs, same library
versions, same everything. The environment drift that used to make "works on my machine"
a genuine excuse evaporates. When FFMPEG or a codec library needs updating, there's one
place to update it.

What I kept out of the Docker story for now: orchestration. We're not running Kubernetes
or ECS clusters yet; the workers run as containers on individual EC2 instances behind
an Auto Scaling group. That's less elegant than a proper scheduler but it's the part
we actually have running. Containerize the hard thing first, build the scheduling
sophistication on top. Don't let the perfect orchestration story stop the useful thing
from shipping.
