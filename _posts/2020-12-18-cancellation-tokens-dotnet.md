---
layout: post
title: "CancellationToken discipline and why you need it in long-running operations"
date: 2020-12-18
author: marcetux
tags: [dotnet, csharp, async, performance]
---
A batch report job that took three minutes started leaving ghost processes on the
server after someone canceled the request mid-run. The request cancellation reached
ASP.NET Core, which set the `HttpContext.RequestAborted` token — but the job's
`Task` was running independently and ignored the cancellation. The job ran for three
minutes, wrote a report nobody would read, and held database connections while it did.

`CancellationToken` is the cooperative cancellation primitive in .NET: the caller
signals intent to cancel; the callee checks and decides whether to honor it. "Cooperative"
is the keyword — the callee must check. The discipline is threading the token through
every async call that has meaningful work to stop: `await dbContext.ToListAsync(ct)`,
`await httpClient.GetAsync(url, ct)`, and periodic `ct.ThrowIfCancellationRequested()`
inside tight loops. When ASP.NET Core cancels the request, the token propagates through
the call tree, and the work stops where it's meaningful to stop rather than running
to completion for no one.

The places engineers forget: background services registered with `IHostedService`
need the `stoppingToken` from `ExecuteAsync` threaded through all their work. Queue
processors need to check cancellation between messages. The rule I follow: if a method
takes longer than a second, it takes a `CancellationToken`. If it doesn't have one,
add it before the call, not after the incident.
