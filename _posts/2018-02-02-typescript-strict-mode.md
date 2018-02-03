---
layout: post
title: "TypeScript strict mode and what it actually catches"
date: 2018-02-02
author: marcetux
tags: [typescript, javascript, frontend, tooling, quality]
---
We've had TypeScript in the Go RN frontend for about six months now and I finally
turned on `strict: true` in the tsconfig. I'd been putting it off because "we'll fix
it incrementally" — which is the way people say "we'll never fix it." A wet Sunday
afternoon, a branch, and about three hours of errors I'm genuinely glad a compiler
found before a user did.

The flag bundles a handful of checks, but the one that does the most work in a
real codebase is `strictNullChecks`. Once the compiler understands that `string` and
`string | null | undefined` are different types, an entire category of runtime errors
becomes a red squiggle. We had a dozen places where we accessed a property on something
that could have been null — things that had survived code review because the reviewers
were reading fast and the types looked fine. They weren't fine.

`noImplicitAny` is the other one that earns its keep on the upgrade path: every place
where you'd silently widened to `any` is now an explicit decision you have to defend.
Most of ours turned into real types within a minute's thought. A few are genuinely
`any` and I left them that way. But now they're deliberate rather than accidental,
and that's the distinction that matters. Turn it on once, fix it fully, never turn it
off.
