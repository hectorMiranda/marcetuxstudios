---
layout: post
title: "TypeScript 2.0 migration in a real codebase"
date: 2016-05-02
author: marcetux
tags: [typescript, javascript, frontend, tooling, migration]
---
The TypeScript 2.0 beta migration I started in April is merged and the discriminated
union pattern I was excited about has survived contact with production data. The
migration itself was less painful than I expected and more tedious than I wanted — the
majority of the effort was adding `| null` and `| undefined` to types that pretended
those values didn't exist, then following the compiler errors to the places that assumed
they didn't exist either.

The useful discipline the migration forced: every API response now has a result type
with an explicit error branch. Before, error handling was checked by convention —
"remember to check `.error`" was a code review comment rather than a compiler error.
After, you cannot call `.data` on a result without first checking that it's the success
variant. The switch statement has branches the compiler verifies are exhaustive. It's
the same principle as the dead-letter queue: make the failure path unavoidable rather
than forgettable.

There's a one-time pain I want to be honest about: if you have a large existing
TypeScript 1.x codebase with no null discipline, the 2.0 migration is a multi-day
effort. You can soften it by enabling `--strictNullChecks` first and fixing the errors
incrementally without upgrading the compiler version, which is what I recommend. The
end state justifies the work; do it in stages rather than all at once.
