---
layout: post
title: "Upgrading to Angular 4 before it was Angular 4"
date: 2017-03-06
author: marcetux
tags: [angular, typescript, frontend, upgrade]
---
Angular 4 released this week, which is confusing because it follows Angular 2 — there
was no Angular 3, because the router package was already at v3 and they decided to
align all package versions rather than explain the gap. I had been tracking the release
candidates, so the upgrade for our project was less dramatic than the jump from Angular
2 RC to final.

The headline for our codebase was the view engine changes and the resulting bundle size
reduction. The generated component code Angular 4 emits is smaller and faster than what
2.x produced — the team claims about 60% reduction in generated code size, and our
build numbers bore that out roughly. No code changes required to get that; it's the
compiler. The `*ngIf` and `*ngFor` improvements with `else` template support also
removed a few awkward structural workarounds we'd written.

The `HttpClient` module deserves a mention too — it's still in progress for this release
cycle, but the direction is clear. The new client handles typed responses, request
interceptors, and progress events in a way the current Http module makes awkward. I
started using it in the next greenfield component rather than waiting for an official
migration guide. The old module isn't going anywhere yet, and parallel adoption is
easier than a big-bang switch.
