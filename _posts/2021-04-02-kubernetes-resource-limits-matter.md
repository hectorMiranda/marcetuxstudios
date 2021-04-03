---
layout: post
title: "Kubernetes resource limits and the noisy-neighbor problem"
date: 2021-04-02
author: marcetux
tags: [kubernetes, azure, devops, reliability]
---
We got paged at 2 a.m. because one service went into a CPU spike and started
starving three unrelated ones on the same node. Classic noisy-neighbor — the
cluster had no resource limits set on any pod, which is the default when you
copy a helm chart from the internet and don't add what the tutorial skips. The
AKS cluster was otherwise healthy. One runaway pod and suddenly the node is at
100% and liveness probes on neighboring services start failing their timeouts.

Kubernetes resource limits do two things: they cap what a pod can consume, and
they let the scheduler understand what a pod *requests* so it can bin-pack
intelligently. If you set neither, the scheduler is guessing and a burst anywhere
can evict neighbors. `requests` sets the soft floor that the scheduler uses for
placement; `limits` sets the ceiling the kernel enforces. Setting limits without
requests is almost as bad as setting neither — the scheduler still doesn't know
what you need, it just knows the ceiling.

The fix we applied in the next hour: added `requests` and `limits` to every
deployment based on p99 metrics from the previous week plus a 30% headroom.
More importantly, we added a `LimitRange` to the namespace so new pods that
omit resource declarations pick up sensible defaults automatically. The tooling
tells you what it's doing if you set it up to tell you; running without limits
is flying blind and calling it trust.
