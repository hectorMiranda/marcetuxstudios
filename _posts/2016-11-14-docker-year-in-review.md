---
layout: post
title: "A year with Docker in a media pipeline"
date: 2016-11-14
author: marcetux
tags: [docker, devops, containers, media, retrospective]
---
I've been running the transcoding workers in Docker containers since February, which is
long enough to have opinions about what the container model does and doesn't improve.
The short answer: the packaging and environment drift problems it solves are real and
it solved them; the operational complexity it introduces is also real.

The wins are clear. The Dockerfile is the spec for the worker environment — FFMPEG
version, codec libraries, runtime — and it's machine-readable and version-controlled.
`docker build` on any machine produces the same image. Promotions from staging to
production are image tag changes, not AMI rebuilds. New engineers can run the full
transcoding stack locally with `docker-compose up`. These are genuine quality-of-life
improvements that I undervalued before I had them.

The surprises: log aggregation is a solved problem (CloudWatch agent on the host, or
pass logs through stdout and collect at the host level) but takes deliberate setup.
Docker's networking adds a layer you don't think about until you're debugging why a
container can't reach a service you thought was on the same network. Image size matters
more than I expected — a 2 GB image with a full FFMPEG build has meaningfully longer
launch times during scale-out than a 400 MB image. The right FFMPEG build for a
production container is smaller than the kitchen-sink build you'd use in development.
Containers are right; treat them as a different kind of thing, not a lighter VM.
