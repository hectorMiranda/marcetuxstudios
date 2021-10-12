---
layout: post
title: "Rust error handling and the question-mark operator"
date: 2021-10-11
author: marcetux
tags: [rust, learning, error-handling, languages]
---
The error-handling model in Rust has been the thing I've worked hardest to
internalize. There are no exceptions in Rust — a function either returns a value or
it returns a `Result<T, E>`. If it can fail, the failure is in the return type,
not in the exception pathway. Coming from C# where `throw` is invisible in the
signature, the explicit `Result` felt verbose until I realized that it's doing the
thing I've always wanted type signatures to do: tell me whether this call can fail.

The `?` operator is the ergonomics layer that makes working with `Result` practical.
`some_fallible_call()?` unwraps the `Ok` value if the call succeeds and returns the
`Err` early if it fails, propagating the error to the caller. The `?` is syntax for
exactly the "if this fails, return the error, otherwise continue" pattern that you'd
write as a match arm every time. A function body that calls three fallible things
in sequence reads almost like synchronous imperative code, with the `?` marks
showing explicitly where failures can occur.

The thing that crystallized for me: Rust forces the question "what happens if this
fails?" at every call site, not just the ones you thought to put a try/catch around.
In C# I've seen entire code paths where a checked exception was thrown and nobody
had a handler for it because the checked exception type got erased somewhere in the
call chain. In Rust, the call chain either handles the error or propagates it to the
surface explicitly. The error can't disappear. It's a different relationship with
failure, and I prefer it.
