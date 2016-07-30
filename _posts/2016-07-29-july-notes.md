---
layout: post
title: "July notes"
date: 2016-07-29
author: marcetux
tags: [meta, retrospective]
---
July was a cost and reliability month. The CDN audit found 13% origin traffic that
shouldn't have been there and the TTL fix dropped the bill by most of that 13%. Immutable
deployments replaced the "SSH and patch" model; the deployment process is now replace-
not-update and the category of "mid-deploy failure" is gone. HTTP/2 is enabled and
delivering the multiplexing benefits; server push stays off until I trust the cache
interaction.

Angular 2 RC has a stable router and I've started a new internal tool in it — the first
new Angular 2 project with a real scope rather than a prototype. The KiCad second board
is at the fab with a BME280 and integrated battery management. I'm more comfortable with
the KiCad workflow each board.

August goal: .NET Core 1.0 on something production-facing rather than just the internal
API experiment. The new Angular 2 tool needs to reach a testable state. And I want to
understand Kubernetes enough to have an opinion on when it's actually worth the
operational overhead, because the conversations about it at work are getting more
frequent.
