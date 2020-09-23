---
layout: post
title: "Kubernetes network policies and the default allow problem"
date: 2020-09-22
author: marcetux
tags: [kubernetes, security, aks, networking]
---
By default, pods in Kubernetes can talk to any other pod in the cluster. That's fine
for a cluster of three pods; it's a problem for a cluster running twelve services where
the batch-report pod has no business initiating connections to the payment service.
Network policies are the declarative firewall that closes the default-allow hole — and
they've been on the backlog since we stood up AKS.

The mental model is a label selector that acts as the policy target, plus ingress and
egress rules that specify what's allowed. A policy that allows the payment service to
receive traffic only from the API gateway and the batch-pricing service — and nothing
else — is three selectors and ten lines of yaml. The cluster enforces it at the CNI
level. A misconfigured service that tries to call payment directly gets a connection
reset; it never reaches the application code.

The rollout strategy: deny all ingress by default in each namespace (a single
empty-selector NetworkPolicy does this), then add explicit allow policies for each
required communication path. Grafana shows the established flows; Splunk catches the
connection resets; together they tell you whether the policy matches what actually
needs to talk. The audit result changed from "pods can talk to anything" to "here
is the exact graph of who talks to whom, enforced by the runtime." That's the kind
of evidence a security review actually accepts.
