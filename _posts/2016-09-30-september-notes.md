---
layout: post
title: "September notes"
date: 2016-09-30
author: marcetux
tags: [meta, retrospective]
---
September was a landmark month for things I'd been watching for most of the year.
Angular 2 final shipped and the internal tool is on the release version — no more RC
caveats. TypeScript 2.0 final makes the `@types` story as simple as npm; the `typings`
tool and the `///reference` gymnastics are gone. Together they're the best TypeScript
development experience I've had.

Kubernetes 1.4 and `kubeadm` moved the "when should we adopt this" line significantly
closer. Not there yet, but closer. RxJS in Angular 2 finally clicked as something more
than "like Promise but different" — the composable teardown is genuinely valuable once
you've had the interval-fires-after-navigation bug one too many times.

October: I want to dig into whether the Let's Encrypt wildcard future changes how I
think about cert management for internal services today — not because I can implement
it, but because the architecture that works well with wildcard certs is probably not
the one that works best with per-domain certs. And I need to think seriously about what
the rest of this year looks like; there's some organizational uncertainty at JibJab that
I'm watching carefully.
