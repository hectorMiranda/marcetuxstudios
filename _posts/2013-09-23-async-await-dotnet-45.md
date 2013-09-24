---
layout: post
title: "async/await in .NET 4.5 and stopping the thread-per-request waste"
date: 2013-09-23
author: marcetux
tags: [dotnet, aspnet, async, webapi, performance]
---
The Web API controllers that hit the database or call upstream services have been
synchronous — each request holds a thread from the ASP.NET thread pool for the entire
duration of the database query or HTTP call. That's fine for low concurrency and
expensive when the call takes 200 milliseconds and there are fifty concurrent requests:
fifty threads sitting idle waiting for I/O, while new requests queue for a thread to
become available.

`async`/`await` in .NET 4.5 fixes this at the language level. Mark the controller action
`async Task<IHttpActionResult>`, `await` the database call (via Entity Framework's async
API) or the HttpClient call, and the thread returns to the pool while the I/O is
in-flight. When the I/O completes, ASP.NET grabs a thread to continue the request —
not necessarily the same thread, but the continuation is correct because `await` captured
the context. The server handles more concurrent requests with the same thread count.

The syntax is the thing Microsoft got right: `await` is a single keyword on an existing
call, not a callback transformation. Turning a synchronous controller action async is
a mechanical change — add `async`, swap `return result` for `return await resultTask`,
change the return type. The mental model flips from "block until done" to "yield the
thread while waiting, resume when done." The performance improvement under load is
genuine; the code is at least as readable as the synchronous version.
