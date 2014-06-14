---
layout: post
title: "Docker volumes and where state lives"
date: 2014-06-13
author: marcetux
tags: [docker, devops, linux, containers, storage]
---
The weekend Docker experiments continued with the question of where stateful data
lives in a containerized setup. The image filesystem is ephemeral — everything written
to it during the container's life disappears when the container stops. For a stateless
web application that's fine and actually desirable. For anything that needs to persist —
a database, uploaded files, log aggregation — you need to handle it explicitly.

Docker volumes are the answer: a path in the container that maps to a path on the host.
Declare `-v /host/data:/container/data` and writes to `/container/data` land on the
host, survive the container's lifecycle, and can be mounted into a new container when
you update and restart. The database data directory, the upload store, the config that
varies between environments — these belong in volumes, not baked into the image.

The corollary is that a well-designed Docker image is immutable: everything in it was
put there at build time and nothing the application writes at runtime should live there.
The image becomes an artifact you can version, audit, and deploy reproducibly. State
lives in the volume, which you manage separately. Separating the application from its
data is the same principle as twelve-factor config, just applied to the filesystem. The
model is clean once you stop treating the container like a persistent virtual machine.
