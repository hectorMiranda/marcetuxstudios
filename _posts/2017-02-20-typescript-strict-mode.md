---
layout: post
title: "TypeScript strict mode in an existing Angular project"
date: 2017-02-20
author: marcetux
tags: [typescript, angular, frontend, tooling]
---
The seller dashboard TypeScript codebase has been accumulating `any` annotations the
way old kitchens accumulate expired condiments — every one of them was a shortcut
someone (sometimes me) took when the types got inconvenient. I turned on
`--strictNullChecks` last week to see what we actually had, and the answer was
"enough to keep two developers busy for a day and a half."

`--strictNullChecks` changes the meaning of `undefined` from "could be anything, don't
worry about it" to a type you have to explicitly handle. Every place we returned an
object-or-null from a lookup and used it without checking — and there were quite a few
— became a compile error. That's not TypeScript being cruel; that's TypeScript pointing
at the production NullReferenceExceptions in waiting. Working through them was tedious
and clarifying in equal measure.

The strategy that works: turn on one strict flag at a time, fix the errors in a bounded
part of the codebase, commit, then expand the scope. Trying to flip everything at once
is a merge conflict factory. `strictNullChecks` first, then `noImplicitAny`, then
`strictFunctionTypes` when you're ready to feel humbled again. The code that comes out
the other end is tighter, and the `any` annotations you keep are intentional ones you
had to decide to keep — which is a much better position than the ones you kept by
accident.
