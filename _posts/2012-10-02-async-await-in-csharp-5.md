---
layout: post
title: "async/await in C# 5"
date: 2012-10-02
author: marcetux
tags: [csharp, async, dotnet, performance]
---

.NET 4.5 shipped with C# 5, and `async`/`await` is the feature I've waited years
for. Asynchronous code that *reads* like synchronous code, without the callback
pyramid or the `Begin/End` ceremony of the old asynchronous programming model.

The reframing that matters: `await` doesn't block a thread, it **releases** it. On
a server, a request waiting on a slow database or an upstream HTTP call used to pin
a thread doing nothing. Await that call instead and the thread goes back to the
pool to serve someone else; your method resumes when the result is ready. For an
I/O-bound web app — which is most of them — that's more throughput from the same
box.

The trap I'm already warning the team about: **`async` is not `parallel`.** Await
doesn't make things run at once; it makes waiting cheap. And `async void` is a trap
outside event handlers — you can't await it, and exceptions vanish. Return `Task`.

This changes how I'll write every API call from here on.

*Update: small runnable example in `examples/2012/async/AsyncDownloads.cs` —
`Task.WhenAll` over a few URLs so the I/O overlaps. Watch the elapsed time.*
