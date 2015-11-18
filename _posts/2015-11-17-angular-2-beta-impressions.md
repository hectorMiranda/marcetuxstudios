---
layout: post
title: "Angular 2 beta impressions from the outside"
date: 2015-11-17
author: marcetux
tags: [angular, javascript, frontend, typescript]
---
Angular 2 went to beta this month and I spent an evening with the examples and the docs.
I'm not migrating anything to it — JibJab is in the middle of a different migration
— but Angular's trajectory is worth understanding because it's where a lot of teams
will land.

The TypeScript integration is thorough in a way that the alpha suggested but didn't
fully deliver. Decorators — `@Component`, `@Injectable`, `@Input` — are how Angular 2
declares components and their dependencies, and they require TypeScript's decorator
proposal (or Babel's experimental decorator support). The component model is coherent:
a class decorated with `@Component` provides a template, inputs, outputs, and lifecycle
hooks. It reads more like a class-based framework than Angular 1's prototype-based
magical binding, and that's an improvement.

The part that made me think hardest: Angular 2 and React are converging on the same
fundamental ideas. Components as the unit of composition, unidirectional data flow by
default, explicit contracts between components. They're arriving there from different
directions — React from a minimal library philosophy, Angular from a full-framework
philosophy — but the destination is similar enough that the choice between them is
increasingly about ecosystem and tooling preference rather than fundamental architecture
disagreement. Both are better than what came before.
