---
layout: post
title: "Service mesh and a first look at Istio"
date: 2018-03-08
author: marcetux
tags: [kubernetes, istio, servicemesh, devops, architecture]
---
Istio went 0.6 this month and I spent a couple of evenings standing it up in the GKE
cluster to understand what "service mesh" actually means below the marketing layer.
The one-line pitch is: mutual TLS, traffic shaping, circuit breaking, and distributed
tracing between your services — without touching service code. The sidecar proxy
handles all of it.

The sidecar model is the part that clicked for me. Envoy gets injected alongside
every pod, intercepts all inbound and outbound traffic, and handles the cross-cutting
network concerns the service itself shouldn't care about. Retry on timeout? Envoy.
Circuit breaker? Envoy. mTLS? Envoy issues and validates the cert from the mesh CA.
The service code stays clean because the proxy is the one doing the work at the
infrastructure boundary.

The honest verdict for a five-person startup: it's too early for us. The operational
weight of Istio — CRDs, control plane components, debugging when something goes wrong
in the proxy layer — is not free, and we don't have enough services for the service-
to-service security story to be urgent. But this is clearly where the industry is
going, and understanding the model now means I'm not reading a tutorial at 2 AM when
a future company hands me a cluster that already has it installed.
