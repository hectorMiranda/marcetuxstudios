---
layout: post
title: "CI/CD for a two-person team on a startup budget"
date: 2017-11-07
author: marcetux
tags: [devops, cicd, aws, startup]
---
The Go RN CI/CD pipeline when I arrived was a manual deploy — SSH into the server, pull
the Docker image, restart the container. That's fine when one person knows the deploy
process. When there are two of us and we need to deploy while one person is in a
customer call, "SSH into the server" is a point of failure waiting to announce itself
at the worst time.

The setup I built is simple on purpose: GitHub pushes trigger AWS CodeBuild, which runs
the tests, builds the Docker image, and pushes it to ECR. On merge to main, a second
CodeBuild job updates the ECS task definition to the new image and triggers a rolling
deployment. The whole pipeline is under five minutes from push to the updated container
running in production. CodeBuild costs almost nothing at our volume.

The thing I made sure of before anything else: the pipeline can be rerun idempotently.
If the ECS update fails, re-triggering the pipeline produces the same result, not a
different one. That means the Docker image tag is the git commit SHA — no "latest," no
timestamp, no ambiguity about what version is running. `ecr.aws/gorn/api:a4f3c12` is
running in production and I can tell you exactly what code that is. Deployments are
boring; finding out you deployed "latest" and don't know what that was is not.
