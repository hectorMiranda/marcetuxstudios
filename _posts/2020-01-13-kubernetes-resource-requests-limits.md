---
layout: post
title: "Kubernetes resource requests and limits are not the same thing"
date: 2020-01-13
author: marcetux
tags: [kubernetes, devops, azure, aks]
---
We deployed a new service to AKS in early January and it started evicting pods under
load. The config had no resource requests or limits, which I'd left blank thinking
"defaults will handle it," and defaults handled it by letting the node run out of
memory and evicting the nearest pod — ours.

The two numbers are doing different jobs. **Requests** are the scheduler's input:
Kubernetes places the pod on a node that has at least that much CPU and memory
available. **Limits** are the runtime's ceiling: if the container exceeds the limit,
the kernel OOM-kills it for CPU throttling or for memory. So requests determine
*where* a pod lands; limits determine what happens when it misbehaves. Setting limits
without requests means the scheduler is flying blind. Setting requests without limits
means a runaway process can eat the whole node.

The practical answer for our APIs: start conservative — request what the service
uses under normal load, limit to roughly double, watch the actual usage metrics for a
week, and tune. The cluster's behavior became predictable the moment the scheduler had
real numbers to work with. It doesn't guess; it does exactly what you tell it. Telling
it nothing is not neutrality, it's just telling it to guess wrong.
