---
layout: post
title: "Kubernetes RBAC and least privilege for real"
date: 2019-04-03
author: marcetux
tags: [kubernetes, security, rbac, devops, banking]
---
The first AKS cluster we stood up had a problem I see in every team new to Kubernetes: everything ran as cluster-admin because that's the identity the person who set it up had, and nobody went back to tighten it. RBAC exists; it just wasn't applied. A penetration test finding with the phrase "lateral movement" in it has a way of fixing that.

Kubernetes RBAC is roles and bindings. A Role (or ClusterRole for cluster-wide scope) declares a list of resources and the verbs allowed on them — `get`, `list`, `watch`, `create`, `delete`. A RoleBinding attaches a role to a subject: a service account, a user, or a group. The discipline is working backward from what something actually needs rather than forward from what's convenient. A payment service needs to read its own Secrets and write its own ConfigMaps. It does not need to list Pods across the cluster or create new Deployments. Writing that out as a Role and binding only the payment service account to it is not paranoia; it's just accurate.

The operational friction is that "what went wrong with the deploy" debugging got harder, because the CI service account can no longer `kubectl exec` into running pods. I consider that a good thing. We gave the developers a separate read-only role for log access and set up proper log forwarding to Splunk. Debugging from logs rather than exec sessions was the right discipline anyway. RBAC doesn't add security by hiding things; it adds it by making the blast radius of a compromised credential smaller and more predictable.
