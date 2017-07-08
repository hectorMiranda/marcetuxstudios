---
layout: post
title: "Kubernetes namespaces for environment isolation"
date: 2017-07-07
author: marcetux
tags: [kubernetes, devops, architecture, tooling]
---
When the third service moved onto the production Kubernetes cluster, the `kubectl get
pods` output became long enough that finding a specific pod took a second of scanning
instead of an instant. That's not a serious problem, but it's the first sign of a
coordination problem that gets worse. The answer is namespaces, and I'd been putting
off using them because the default namespace was working fine.

A Kubernetes namespace is a logical partition within a cluster — resources in one
namespace don't conflict with resources of the same name in another. The obvious use
is environment isolation: staging and production in the same cluster but different
namespaces. Resource quotas per namespace prevent a runaway staging deploy from
consuming cluster capacity that production needs. RBAC can scope access by namespace —
developers have full access to staging, read-only to production, which is the access
model I want.

The migration from the default namespace to a proper namespace layout took an afternoon:
rename the manifests, add a `namespace:` field, update the Helm values, deploy in order.
The one thing that trips people up — and tripped me up — is that Kubernetes Services
are namespace-scoped. A pod in the `production` namespace reaching a service in `staging`
has to use the fully qualified DNS name: `service.staging.svc.cluster.local`. Within a
namespace you use the short name; across namespaces you use the full one. That's
documented, but the first cross-namespace connection failure is how you actually learn it.
