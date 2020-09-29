---
layout: post
title: "September notes"
date: 2020-09-28
author: marcetux
tags: [meta, retrospective]
---
September felt like consolidation. The .NET 5 RC is solid enough to commit to — I'm
using records everywhere the pattern fits and finding more places every week. System.Text.Json
is on the next new service. The ADO pipeline templates cut the maintenance surface
for a class of changes from six files to one.

The infrastructure security work continues. Network policies on AKS mean the cluster
has an explicit allow graph now rather than implicit allow-everything. Combined with
the OPA admission policies from July and the mTLS from March, the security posture
is meaningfully different from where we started the year. The individual pieces aren't
dramatic; the composition is.

The Pi cluster with Prometheus is the lab win. I can see Longhorn replication traffic,
sensor stack query latency, and MQTT message throughput in the same dashboard. It
looks like the work infrastructure, just smaller and entirely mine. October: .NET 5
is likely to RC2 or release candidate final before November. I'll write up the
migration guide for the team based on what I've learned in preview.
