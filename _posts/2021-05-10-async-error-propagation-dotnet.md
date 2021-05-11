---
layout: post
title: "Async error propagation and where exceptions go"
date: 2021-05-10
author: marcetux
tags: [dotnet, csharp, async, debugging]
---
A production incident last week came down to a swallowed exception in an async
method. The method returned `async void` — which you should almost never do in
library code — and the exception it threw went to the `ThreadPool` exception handler
rather than the caller. The service kept running, the operation silently failed, and
we found out from a customer report twenty minutes later rather than from an alert.
It's a known footgun, and we still stepped on it.

The rule is almost always `async Task` or `async Task<T>`, not `async void`.
The only legitimate use for `async void` is event handlers, because the event
delegate signature is fixed. In everything else, `async void` is `async Task` where
the task is orphaned: the caller can't await it, can't observe its completion, and
can't catch its exceptions. The exception goes up to the synchronization context or
to the thread pool and becomes an unhandled exception — in .NET 5 and later that
unhandled exception will crash the process, which is actually better behavior than
the silent failure we got, but neither is what you wanted.

The fix is mechanical: `async void` becomes `async Task`, callers that ignored the
return value start awaiting it. The audit took an afternoon. What's harder to fix
is the habit of reading return types without asking what happens to errors. For
async code the return type is the error-propagation contract, and `void` means "I
discarded it." That's rarely what you meant.
