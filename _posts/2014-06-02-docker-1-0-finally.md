---
layout: post
title: "Docker 1.0 ships and I finally have to take it seriously"
date: 2014-06-02
author: marcetux
tags: [docker, devops, linux, containers, infrastructure]
---
Docker hit 1.0 at DockerCon this week, which is the moment a project transitions from
"interesting experiment" to "you should probably know what this is." I'd poked at it in
early 2013 when it was first released and filed it under "Vagrant for Linux processes,
probably useful later." Later is apparently now.

The idea is containers: isolated processes that share the host kernel but have their own
filesystem layer, network namespace, and process tree. The difference from a VM is that
there's no guest OS — the container runs against the host kernel directly, which makes
startup nearly instant and memory overhead minimal compared to a full virtual machine.
The Docker image format solves the packaging problem: everything the application needs
lives in a layered image that can be pushed to a registry and pulled anywhere Docker
runs. The same image that passes tests in CI runs in production, and the "works on my
machine" problem becomes a different kind of problem — one you can actually inspect.

I spent the weekend running it on Ubuntu because Docker on OS X and Windows still
required a Linux VM underneath (boot2docker). The build-run-inspect loop is fast and
the Dockerfile is readable in a way that most build systems aren't. I don't have a
production use case yet, but I understand now why everyone started talking about this
in the same breath as microservices. Packaging the unit of deployment with its
dependencies is the right idea.
