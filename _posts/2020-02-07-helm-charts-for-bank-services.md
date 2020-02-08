---
layout: post
title: "Writing Helm charts that a teammate can actually understand"
date: 2020-02-07
author: marcetux
tags: [kubernetes, helm, devops, aks]
---
The first Helm chart I wrote for the bank's AKS cluster was a masterclass in
unnecessary cleverness. Nested conditionals, six layers of template helpers,
a `values.yaml` with forty keys where twelve would have been enough. It worked. A
new teammate who needed to change a port spent two hours reading it first.

The thing I'd gotten backwards: a Helm chart is not a Kubernetes YAML generator;
it's a configuration interface for your service. The templates are the implementation
detail; `values.yaml` is the surface your team touches. Keep the surface flat and
obvious. Each value should have a name a tired person can guess and a default that
makes the chart runnable out of the box. If a value is bank-specific and always
gets overridden, it should still have a sane placeholder so `helm template` produces
valid output for local inspection.

The rewrite of that chart is forty lines shorter. The teammate changed the port in
ninety seconds. The test I now apply before calling a chart done: can someone who
didn't write it make the most common change — image tag, replica count, environment
variable — with no guidance? If the answer is "they need to read the templates first,"
the values surface needs more work. The chart isn't for me.
