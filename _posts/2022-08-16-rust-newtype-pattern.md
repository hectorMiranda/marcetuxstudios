---
layout: post
title: "The newtype pattern in Rust for safer domain types"
date: 2022-08-16
author: marcetux
tags: [rust, design, types, programming]
---
The unit confusion problem is old — NASA lost a Mars orbiter because one team used
metric and another used imperial, and software has its own version of that mistake
constantly. In blockchain code the stakes are real: a `u64` that represents motes
should never be passed where a `u64` that represents a token amount is expected, and
the types `u64` and `u64` are not going to stop you.

The newtype pattern in Rust is the fix. A newtype is a struct with a single field and
no `#[repr(transparent)]` overhead: `struct Motes(u64)` and `struct TokenAmount(u64)`
are distinct types with no implicit conversion between them. The compiler now catches
the confusion: if a function takes `Motes` and you pass `TokenAmount`, it won't compile.
You add the conversion explicitly where it's actually correct — `Motes(token_amount.0)`
— and that explicit conversion is where you'd think to add the unit-correctness check
or documentation.

In the token contract code, Motes and TokenAmount being distinct types caught two
bugs during a refactor where I was restructuring the payment validation path. Both
bugs were places where the calculation was correct but the units were being mixed up —
the kind of bug that passes all the tests if your tests don't verify the unit semantics
explicitly. The type system did the work the tests would have had to do. New types are
cheap to define and the safety is free; the cost is discipline in naming and the
occasional explicit cast. Worth it every time.
