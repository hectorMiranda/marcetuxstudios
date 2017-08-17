---
layout: post
title: "Angular 5 RC and what actually matters for existing projects"
date: 2017-08-16
author: marcetux
tags: [angular, typescript, frontend, upgrade]
---
Angular 5 hit release candidate in August and I've been running the RC against the
seller dashboard to see what actually needs attention before the final release. The
Angular team has maintained a good track record on non-breaking upgrades since the
Angular 4 release, so my default expectation is "run `ng update`, fix a few deprecation
warnings, done" — and mostly that's still true.

The genuinely new things in 5 that I care about: `HttpClient` promoted to stable, which
means the old `Http` module is now formally deprecated. We've been using `HttpClient`
in new code since April but the legacy components still use the old module, so the 5
release is the forcing function to finish that migration. The HTTP interceptor pattern
from May works with `HttpClient` and not with the old `Http`, so every interceptor we
wrote is already on the right side.

The build optimizer is the other meaningful change — a production build flag that
applies additional tree-shaking and decorator removal that the Angular compiler can
validate as safe. The bundle difference on the seller dashboard: about 8% smaller
vendor bundle, 11% smaller app bundle. I'll take it for a flag in the build script.
The performance improvements in Angular's change detection for animations and pipes
are real but harder to benchmark against a production dashboard that doesn't lean on
animations.
