---
layout: post
title: "ES2016 is almost nothing and that is the point"
date: 2016-01-22
author: marcetux
tags: [javascript, es2016, frontend, language]
---
The TC39 working notes and the general chatter make clear that ES2016 — the version
after ES2015's enormous release — will be tiny: `Array.prototype.includes` and the
exponentiation operator `**`. That's essentially it for language-level additions. Coming
off ES2015 with its classes, arrow functions, destructuring, Promises, modules, and
generators, ES2016 feels anticlimactic. It's not. It's intentional.

The new release cadence is yearly and small: ship what's ready, nothing more. It's the
opposite of ES5→ES6/ES2015, which accumulated a decade of proposals and shipped a
language that took another two years to be safe to use without a transpiler. Annual
releases mean the spec advances faster than tooling can stall. ES2016 being small
doesn't mean the next one is small — it means things get shipped when they're done
rather than held for the next big-bang release.

For day-to-day work, `includes` is genuinely useful: `[1, 2, 3].includes(2)` reads
like it should, handles `NaN` correctly unlike `indexOf`, and replaces an idiom I've
written hundreds of times. The `**` operator I'll probably use twice. But both will
ship in V8 within months, TypeScript will support them, Babel already does, and I
won't have to think about it. Small spec, fast tools, boring update. I'll take it.
