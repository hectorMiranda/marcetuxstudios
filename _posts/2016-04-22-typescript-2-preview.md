---
layout: post
title: "TypeScript 2.0 is coming and the type system grew up"
date: 2016-04-22
author: marcetux
tags: [typescript, javascript, frontend, language, tooling]
---
The TypeScript 2.0 beta has been available for a few weeks and I've been running it
against the side projects. The big addition is non-nullable types as a first-class
feature of the type system — `--strictNullChecks` is now a proper flag rather than a
hack on top of the 1.x system, and the compiler enforces it consistently. The 1.8
version I wrote about in January was already useful; 2.0 makes it correct in places
where 1.8 still had holes.

The other thing I'm excited about is the improved `readonly` support and tagged union
types — the ability to discriminate between union members using a literal type field.
A type that is `{ kind: "success", data: T } | { kind: "error", message: string }` is
a result type that the compiler understands how to narrow. You switch on `kind` and
inside each branch the compiler knows which union member you have. It's the ML-style
discriminated union pattern, and it makes modeling the success/failure boundary
explicit in a way that `try/catch` doesn't.

The practical impact at work: I've already started migrating the API response types to
discriminated unions in the TypeScript 2.0 branch. The code that was "here, check
`.error` if it might have failed" becomes "the type system won't let you skip the
error case." That's the kind of upgrade I'll pay the migration cost for.
