---
layout: post
title: "C# async patterns worth knowing in 2025"
date: 2025-07-16
author: marcetux
tags: [csharp, dotnet, async, backend, performance]
---
A client greenfield in C# had a senior dev who'd been writing async code for five years
and hadn't updated his patterns since the early async/await years. Not a criticism — the
old patterns work — but .NET has added enough ergonomics that the gap between "correct
old way" and "correct current way" is now large enough to matter for maintainability.

The pattern worth revisiting first is `ConfigureAwait`. The old guidance was
"always use `ConfigureAwait(false)` in library code." The current guidance is more
nuanced: in library code it still applies; in application code (ASP.NET Core, for
example) the synchronization context that made it necessary doesn't exist by default,
so you can drop the noise. Fewer `ConfigureAwait(false)` annotations that don't do
anything is code that's easier to read. Understand when it matters, apply it there,
leave it off where it doesn't.

The newer pattern worth adopting is `IAsyncEnumerable<T>` for streaming results. If
you have a database query that returns a large set, a pipeline that produces items
incrementally, or an LLM feature that streams completions, `IAsyncEnumerable` is
the right abstraction — the caller processes each item as it arrives instead of
buffering everything first. It's been in the runtime since .NET Core 3.0 and I still
see teams buffering to `List<T>` by habit. Async enumerables; let the work flow.
