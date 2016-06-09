---
layout: post
title: "Docker Compose for a consistent local dev environment"
date: 2016-06-08
author: marcetux
tags: [docker, devops, developer-experience, local-dev]
---
The transcoding pipeline depends on SQS, S3, and DynamoDB, none of which you want to
hit in production while developing locally. For a long time the solution was environment
variables that pointed at a staging prefix "and don't run the real consumer." That works
until someone forgets the "don't run the real consumer" part. Docker Compose is the
correct fix.

A `docker-compose.yml` defines the services that make up the local environment: the
application container, LocalStack (which emulates SQS, S3, and DynamoDB locally), a
Redis container for rate limiting, and a small init container that creates the queues
and buckets in LocalStack before the app starts. `docker-compose up` brings the entire
stack to life in the right order. The app is talking to LocalStack endpoints instead of
real AWS; nothing can accidentally reach production resources because the endpoints
point at localhost.

The developer experience improvement is real: `git clone`, `docker-compose up`, you have
a running environment in two minutes. No "install this, configure that, manually create
the dev queue" onboarding document that's always out of date. The Compose file is the
document. It's also what I'd call boring at the boundary: a tired new engineer at 11pm
following setup instructions can't accidentally misconfigure the environment because the
environment is code and the code is checked in.
