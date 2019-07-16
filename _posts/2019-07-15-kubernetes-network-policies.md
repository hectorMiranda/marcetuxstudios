---
layout: post
title: "Kubernetes network policies and microsegmentation"
date: 2019-07-15
author: marcetux
tags: [kubernetes, security, networking, banking, devops]
---
Kubernetes networking default is flat: any pod can talk to any other pod. For a dev cluster running hobby apps that's fine. For a bank's AKS cluster where the payment service and the admin tooling are in the same cluster, it is not fine. Network policies are how you say "these pods may talk; those may not," and we finally applied them systematically.

A NetworkPolicy is a Kubernetes resource that selects pods by label and defines which ingress and egress traffic is allowed. The approach we took: default-deny everything, then add explicit allow rules for each communication path we actually need. The payment service gets ingress from the gateway (labeled `component: gateway`) on port 443 and egress to SQL Server on port 1433 and to Service Bus on port 5671. That's it. Not the admin namespace, not the monitoring namespace — those have their own explicit rules. If a pod needs to talk to something, someone has to write a policy permitting it.

The operational uplift is that new services are isolated by default. A developer stands up a new service, it can't reach anything until a policy allows it, and the policy becomes a reviewed artifact. That's the inverse of the previous model where everything worked until someone said it shouldn't. The audit conversation flips: we're not explaining why some traffic was blocked; we're explaining why each traffic path was explicitly permitted. In a compliance context, that's the conversation we want.
