---
layout: post
title: "Azure App Services and the managed platform tradeoff"
date: 2018-07-10
author: marcetux
tags: [azure, cloud, devops, aspnet, architecture]
---
CTM is partially migrating reporting-tier workloads to Azure, and my first hands-on
time with App Services confirmed what I expected: it's the right choice for workloads
where you want to ship a web application without caring about the underlying VM. You
deploy a package or a container, configure app settings and connection strings through
the portal or ARM templates, and the platform handles OS patching, load balancing,
and basic autoscaling. The operations surface is small and the operations team can
manage it without owning a cluster.

The things that surprised me: the deployment slots for staging environments are
genuinely good. You push to a staging slot, smoke test it, and swap it to production
in a few seconds with automatic traffic cutover and rollback. That's the zero-downtime
deploy pattern I'd been building myself in Kubernetes, available out of the box for
workloads that don't need container orchestration. For a .NET WebAPI with moderate
traffic and a predictable scaling curve, App Services fits.

The edge cases where it shows strain: any workload with unusual file system access,
long-running background processes that the platform is likely to recycle, or latency
requirements that need instance warm-up baked into the routing. The platform is
opinionated about the stateless, request-response pattern, and anything that fights
that opinion will eventually lose. Understand the constraints before you commit to
the tier.
