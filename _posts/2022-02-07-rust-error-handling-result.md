---
layout: post
title: "Rust error handling with Result and the question mark operator"
date: 2022-02-07
author: marcetux
tags: [rust, error-handling, programming]
---
Error handling in Rust is the thing that feels most alien to a C# developer, right up
until it doesn't. C# throws exceptions; you either catch them somewhere in the call
stack or the runtime catches them for you and the program dies. The Rust position is
that recoverable errors should be explicit in the type system — a function that can fail
returns `Result<T, E>`, which is either `Ok(value)` or `Err(error)`, and the caller
must deal with both cases.

The question-mark operator — `?` — is what makes this ergonomic instead of exhausting.
In a function that returns `Result`, you can write `let x = something_that_fails()?`
and the `?` will propagate the error upward as a return value if it's an `Err`. The
call chain reads almost like normal code, but the compiler has verified that every
possible failure has been accounted for by the time it reaches the top-level caller.
There's no silent exception swallowing, no catch-all handler hiding problems, and no
`NullReferenceException` hiding in a field you forgot to initialize.

What I'm finding in the contract code is that this model forces better design. A
function that returns `Result<(), ContractError>` with a well-typed error enum is a
function whose failure modes are part of its interface, not implementation details that
leak sideways. The question-mark operator is a small syntactic convenience that
represents a much larger commitment to making errors visible.
