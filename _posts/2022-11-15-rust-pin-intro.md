---
layout: post
title: "Rust Pin and when you finally need it"
date: 2022-11-15
author: marcetux
tags: [rust, async, pin, advanced]
---
You can write a lot of async Rust without understanding `Pin`, and then one day you're
implementing a manual `Future` or dealing with a self-referential struct in an async
context and `Pin<P<T>>` is everywhere in the type signatures. The concept is not
difficult once you understand what problem it solves, but the motivation is non-obvious
if you arrive at it from user code rather than from implementing a runtime.

The problem `Pin` solves: async state machines in Rust can be self-referential. A
generated future that represents a suspended async function might contain a reference
to another field within itself — the future holds both a buffer and a reference into
that buffer, for example. In normal Rust, self-referential types are trouble because
if you move the outer struct in memory, the internal reference is now dangling. `Pin`
is the guarantee that the value behind it will not be moved after it's pinned.

The `Pin<Box<T>>` and `Pin<&mut T>` types appear in the `Future::poll` signature —
`fn poll(self: Pin<&mut Self>, cx: &mut Context<'_>) -> Poll<...>` — because the
runtime needs to poll a future without being allowed to move it. For user code writing
normal async functions, the compiler handles pinning automatically. For the indexer
work where I've been implementing custom `Stream`s, I'm meeting `Pin` in the function
signatures and occasionally in unsafe code I'm carefully not writing myself, relying
instead on `pin_project` to derive the safe projection.
