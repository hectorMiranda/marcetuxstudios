---
layout: post
title: "Mutual TLS between services and why the mesh handles it better than you do"
date: 2020-03-05
author: marcetux
tags: [kubernetes, security, azure, architecture]
---
A compliance review flagged inter-service traffic inside AKS as unencrypted, which
was correct — pods on the same cluster talking HTTP over a virtual network felt
safe until someone pointed out that "felt safe" is not an audit-passing posture.
The options were: add TLS termination to every service individually, or use the
mesh to handle it automatically.

We looked at Istio and Linkerd. Istio is the full-featured option with the
correspondingly heavy footprint; Linkerd is focused, smaller, and its automatic
mTLS story is as close to zero-config as service-mesh software gets. You install
the control plane, inject the data plane sidecar into your pod spec, and inter-pod
traffic is encrypted and mutually authenticated without touching application code.
The certificate rotation is also automatic — no manual cert management, no expiry
incident.

We're piloting Linkerd on one namespace. The overhead per pod is real but not
alarming — the sidecar adds memory and a few milliseconds of latency at the tail.
For a compliance requirement that was otherwise going to mean TLS boilerplate in
a dozen services, the trade is obviously worth it. The mesh earns its complexity
tax when it removes more complexity than it adds.
