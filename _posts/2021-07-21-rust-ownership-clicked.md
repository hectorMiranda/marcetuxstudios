---
layout: post
title: "The Rust ownership model clicked during a real problem"
date: 2021-07-21
author: marcetux
tags: [rust, learning, systems, languages]
---
I hit a concurrency bug in a small Rust side project — two threads sharing a data
structure — and instead of getting a runtime panic or a race condition that
manifests intermittently, I got a compile error explaining exactly what was wrong.
The error pointed at the line that tried to move a value into a closure while
a reference to that value still existed in the outer scope. The borrow checker
caught a threading bug at compile time. I had to read it twice before I believed it.

In any other language I'd reach for a mutex and trust myself to hold it correctly.
Rust's `Arc<Mutex<T>>` is the equivalent, but the difference is what happens when
you hold it wrong: the compiler won't let you access the value inside the mutex
without holding the lock, not as a convention but as a type-system constraint. You
literally cannot write "access data without locking" code that compiles. The
class of bug where a developer forgot to acquire the lock — or acquired the wrong
one — doesn't exist in valid Rust.

I'm not ready to advocate for Rust at work; the learning curve is real and the
C#/.NET ecosystem serves the day job correctly. But the mental model is changing
how I think about ownership in C# — who owns this object, who has references to it,
when is it safe to mutate. The borrow checker is not a tax on writing code; it's
a way of making implicit contracts explicit so the compiler can verify them. I want
more of that, in whatever language.
