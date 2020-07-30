---
layout: post
title: "July notes"
date: 2020-07-29
author: marcetux
tags: [meta, retrospective]
---
July split evenly between new things and fixing structural gaps. C# 9 records are
already in the small internal tools I can use them in — the convenience is real,
not just impressive. Terraform drift detection is running nightly and has already
caught two manual changes that would otherwise have been lost on the next apply.
OPA is in the AKS admission chain for the most critical policies; more will follow
as I write Rego I'm confident in.

The solar monitor is the home lab win of the month. Seeing production and consumption
on the same dashboard, with a net line, has changed how I think about the house's
energy story. The summer afternoon export windows are significant. I'm already
sketching a simple rule that could shift some consumption into those windows — running
the dishwasher after the panels peak rather than before — but that's a behavior change,
not a firmware change.

August: the .NET 5 RC schedule suggests previews through September, RC in October.
I want to look at gRPC improvements in .NET 5 for the inter-service calls inside the
cluster, where the JSON serialization overhead is measurable and gRPC's binary
protocol and generated clients would be a clean fit.
