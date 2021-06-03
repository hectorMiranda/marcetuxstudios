---
layout: post
title: "Service mesh skepticism from the trenches"
date: 2021-06-02
author: marcetux
tags: [kubernetes, service-mesh, architecture, reliability]
---
We evaluated Istio for mutual-TLS between services in the AKS cluster, and after
three weeks of prototype I'm going to write down why we decided not to deploy it in
production. Not because Istio is bad software — it isn't — but because the cost-
benefit calculation doesn't clear the bar at our scale and with our team.

The promise is compelling: automatic mTLS, traffic shaping, circuit breaking, and
detailed telemetry, all without modifying application code. The sidecar proxy
intercepts all traffic and the application doesn't know it's there. In practice,
"the application doesn't know it's there" means "the application can't reason about
what it's there doing," and the debugging experience when something goes wrong in
the proxy layer is substantially worse than debugging something in code you wrote.
The Envoy configuration surface is a language unto itself.

For our cluster of a dozen services, Azure's built-in network policy plus mutual-TLS
certificates managed through cert-manager gives us the thing we actually needed —
verified identity at the network boundary — without the operational surface area of
a full mesh. Circuit breaking and retries live in the application code where the
business logic that informs those decisions also lives. I'm not ruling out a mesh
forever; I'm ruling it out for now, for this team, at this size. Complexity is
a cost that compounds over time and has to earn its keep at the moment you pay it.
