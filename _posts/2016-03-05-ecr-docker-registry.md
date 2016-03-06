---
layout: post
title: "Moving Docker images to ECR and out of shared AMIs"
date: 2016-03-05
author: marcetux
tags: [docker, aws, ecr, devops, pipeline]
---
The transcoding worker containers I built in February were being distributed the wrong
way: the Docker image was baked into an EC2 AMI with Packer, and the AMI was shared
across staging and production. That meant every image update required an AMI rebuild,
and staging and production diverged whenever someone forgot to rebuild both. The right
container workflow is a registry.

Amazon ECR is the path of least resistance when you're already on AWS. It's a private
Docker registry with IAM integration — EC2 instances with the right role can `docker
pull` without credentials stored anywhere, and the registry is in the same region so
pulls are fast and free of data transfer charges. The push is standard Docker: `aws ecr
get-login-password | docker login`, tag the image with the ECR repository URI, push.
The Auto Scaling launch configuration references the ECR URI; instances pull the image
on startup.

What this fixed: the staging and production AMIs are now identical and version-pinned
to a specific image tag, not to whatever was in the AMI at build time. A staging test
proves the image; the same image tag deploys to production. The AMI still exists —
you need *something* to launch — but it's stripped down to "knows how to pull and run
a Docker container" rather than containing the application. Separate the base from the
app, version the app independently.
