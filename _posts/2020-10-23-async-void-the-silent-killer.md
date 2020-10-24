---
layout: post
title: "async void and why it silently kills your exception handling"
date: 2020-10-23
author: marcetux
tags: [dotnet, csharp, async, debugging]
---
A background task in a services controller was silently swallowing exceptions. The
task would fail, the UI would look fine, and three seconds later the downstream
service would be in an inconsistent state. Reading the code: `async void FireAndForget()`.
That was the bug.

In C#, `async void` methods cannot be awaited. If they throw, the exception is raised
on the synchronization context — in an ASP.NET Core app, that means it surfaces on
the thread pool, not inside any try/catch in the calling code, and in many configurations
it crashes the process. But more commonly in server code, the exception gets swallowed
by the framework's unhandled exception handler without appearing in the caller's error
handling. The caller proceeds normally. The exception is gone.

The fix is never `async void` except for event handlers where the framework forces it.
Anything else: `async Task`, awaited or tracked with `Task.Run` plus explicit
`ContinueWith` or a similar mechanism. If you genuinely want fire-and-forget with
exception logging, write an extension method that wraps the task, awaits it on a
background continuation, and logs any exception to your logger. Make the behavior
explicit in code. `async void` is the implicit behavior that bites you three weeks
after you wrote it.
