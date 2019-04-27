---
layout: post
title: "Service mesh and when not to reach for it"
date: 2019-04-26
author: marcetux
tags: [kubernetes, architecture, service-mesh, istio, tradeoffs]
---
Istio came up in the architecture review this month as a solution to our mTLS-between-services problem. I've been skeptical since I saw the first demo, and I finally had to articulate why, because "I don't like the complexity" is not an architecture argument.

The argument against Istio for us right now: the problems it solves are real — mTLS, circuit breaking, traffic management, observability — but we've already solved most of them with lighter-weight tools. mTLS is cert-manager plus the gateway pattern. Observability is App Insights plus Dynatrace. Circuit breaking is Polly in the application layer. A service mesh adds a sidecar to every pod, a separate control plane, and an additional operational domain for the team to understand and debug. When the mesh misbehaves — and it will — the people on call need to know enough about Envoy and the Istio control plane to diagnose it at 2 AM.

The conclusion I landed on: Istio is the right answer when you have many services, many teams with different languages and stacks, and you need consistent policy enforcement across all of them without asking every team to implement it themselves. We have mostly .NET, a small team, and shared libraries that enforce our patterns. The shared library is lower overhead than the sidecar. When we add teams with different stacks or hit a problem the shared library can't solve uniformly — revisit. "Not yet" is not "never." It's knowing where you actually are on the complexity curve.
