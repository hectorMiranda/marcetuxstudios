---
layout: post
title: "AKS node pools and workload isolation"
date: 2019-02-08
author: marcetux
tags: [kubernetes, azure, aks, architecture, devops]
---
Running Kubernetes in Azure for a while now, and the thing I keep having to explain to people is why we can't just throw all workloads onto one big node pool and call it a day. The answer is isolation — regulatory, resource, and operational — and AKS node pools are how we express it.

A node pool is a set of VMs with a specific size, OS image, and taint profile. We run three: a system pool for AKS internal components, a standard pool for most application workloads, and a compliance pool with more restrictive network policies, enhanced disk encryption, and tighter egress rules for anything that touches customer financial data. A workload declares which pool it wants via node selectors and tolerations in its pod spec; the scheduler does the rest. Compliance reviewers can audit "what runs on the compliance pool" as a concrete list rather than a policy aspiration.

The operational benefit is that a runaway workload on the standard pool can't steal resources from the compliance-tier services. We size the compliance pool conservatively so its pods have guaranteed headroom. The cost is higher because we're not bin-packing as aggressively, and I've had to defend that to the finance team more than once. The answer is: the alternative is a noisy neighbor incident during a payment processing window, and that conversation costs more than the extra VMs.
