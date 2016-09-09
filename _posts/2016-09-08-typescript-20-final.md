---
layout: post
title: "TypeScript 2.0 final and what changes for real projects"
date: 2016-09-08
author: marcetux
tags: [typescript, javascript, frontend, language, tooling]
---
TypeScript 2.0 shipped final the same week as Angular 2 — September 22 — and the
combination is the best integrated TypeScript experience I've had. I wrote about the
2.0 beta in April and the discriminated union types in May; the final release adds a few
more things that quietly improve the day-to-day.

`@types` is the big ecosystem change. Type definitions for third-party packages used to
live in the `typings` tool and a separate registry; 2.0 moves them to npm under the
`@types` scope. `npm install --save-dev @types/lodash` and lodash is typed, no other
tool required. The `typings` file and the `///reference` lines at the top of every file
are gone. It's the kind of friction you only notice once it's removed.

The `readonly` property modifier and `never` type round out the story. `readonly`
enforces immutability at compile time on object properties — not deep immutability, but
"you can't reassign this property" which catches a class of bugs in reducer-style code.
`never` is the type for code paths that can't be reached, which makes exhaustive switch
statements on discriminated unions compiler-verified: if you forget a case, the
`default` branch hits a `never` and the compiler errors. The type system closes the
exhaustiveness gap. That's the kind of guarantee I used to test for instead of model.
