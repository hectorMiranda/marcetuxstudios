---
layout: post
title: "TypeScript strict null checks and what they actually catch"
date: 2016-01-13
author: marcetux
tags: [typescript, javascript, frontend, tooling]
---
TypeScript 1.8 shipped `--strictNullChecks` in November and I've been running it on our
front-end build for a few weeks. The short version: it found real bugs I hadn't noticed,
and the churn to add `| null` and `| undefined` annotations was smaller than I expected.

The flag changes `null` and `undefined` from assignable-to-everything into explicit
union members. Before the flag, nothing stops you from assigning `null` to a `string`
and then calling `.toUpperCase()` on it — the runtime crash happens at the user's
cursor, not at compile time. With it on, the type of a nullable value is `string | null`
and you must check before you use it. The compiler flags every place you assumed
something wasn't null. Most of those were "of course it's always set" assumptions that
had no enforcement behind them.

The wins came from API boundaries: the function that looks up a user by ID doesn't
return `User`, it returns `User | undefined`, and every call site that ignored the
"not found" case is now a compile error. The idiom I've settled on is narrowing at
the boundary — check once, extract the non-null value, proceed. The interior of a
function stays clean; the boundary is explicit. It's the same pattern I use for error
handling: make the unhappy path unavoidable rather than easy to forget.
