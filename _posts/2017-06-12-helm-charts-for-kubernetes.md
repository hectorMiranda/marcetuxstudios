---
layout: post
title: "Helm charts and the right level of abstraction for Kubernetes configs"
date: 2017-06-12
author: marcetux
tags: [kubernetes, helm, devops, tooling]
---
Once you have more than two or three Kubernetes deployments, the YAML duplication
problem starts. Every service has a Deployment, a Service, a ConfigMap, and probably
an Ingress — all with 95% identical structure and 5% service-specific values. Copy-
pasting YAML and hoping you remember to change the image name everywhere is where
config drift lives.

Helm is the package manager for Kubernetes that solves this — a chart is a templated
collection of Kubernetes resources, and `values.yaml` is where the per-deployment
overrides live. Install a chart with different values and you get a different
deployment. The templating is Go templates, which is not the most readable syntax for
YAML, but it works and there's a large catalog of community charts for common
infrastructure (RabbitMQ, PostgreSQL, Redis) that you'd otherwise manage by hand.

The judgment call is what to templatize. If a value changes per environment, it goes in
`values.yaml`. If it's truly constant across all environments (like the container port
your app always listens on), hardcode it in the template. The mistake I see is over-
templating: making every field a variable and ending up with a `values.yaml` that's
longer than the original YAML was. Template the variation, not the constants. The
chart for the feed transformation service is 80 lines of templates and 12 lines of
values; the deployment manifest it replaced was 65 lines of YAML that got copied once
per environment.
