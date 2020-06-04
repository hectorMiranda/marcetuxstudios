---
layout: post
title: "Upgrading AKS without dropping requests"
date: 2020-06-03
author: marcetux
tags: [kubernetes, aks, azure, devops]
---
The AKS cluster we've been deferring an upgrade on was two minor versions behind,
which is fine until a CVE lands that's only patched in current. June was the window
to do it properly. The naive approach — upgrade the control plane, then the node
pools, hope — works in staging where nobody is watching. In production, "hope" is
not a strategy for a payment-processing cluster.

AKS rolls node pool upgrades one node at a time by default: cordon, drain, replace,
uncordon. If your pods have proper `PodDisruptionBudgets` configured, the drain
respects them — it won't evict a pod if doing so would take the replica count below
the minimum. If they don't, the drain proceeds anyway and your service shudders. So
the upgrade prep is PDB review: every Deployment that handles traffic should declare
`maxUnavailable: 1` and `minAvailable: N-1`. That's it. The upgrade then becomes
boring node-by-node rotation that Kubernetes manages.

The actual upgrade took two hours, zero dropped requests (verified in Splunk), and
one pod that refused to drain because we'd forgotten to set a PDB on a batch job.
We fixed it, drained, continued. The lesson: the upgrade ceremony is mostly the work
you should have already done. The upgrade is just the moment you find out whether
you did it.
