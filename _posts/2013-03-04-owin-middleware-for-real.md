---
layout: post
title: "Writing a real OWIN middleware piece"
date: 2013-03-04
author: marcetux
tags: [dotnet, owin, katana, aspnet, middleware]
---
The OWIN/Katana post from February was a "I understand it now" post. This is the
"I built something real with it" post. I wrote a request-timing middleware that logs
each request's duration, the matched route, and the status code to a Redis key — no
NuGet packages beyond Katana's core and the Redis client.

The shape of OWIN middleware is dead simple: a class with an `Invoke(IDictionary<string,
object> env)` method that calls the next middleware in the chain or short-circuits.
Everything the request carries — verb, path, response status — is in that environment
dictionary. My piece snaps a `Stopwatch` before calling `next(env)`, then logs the
elapsed time and status on the way back out. It doesn't touch `HttpContext`. It doesn't
care what host is running it. A test just populates the dictionary and calls `Invoke`.

What makes this feel right is the composition. I slot it before the routing middleware
and after the auth middleware — order is just where you register it in `Startup.cs`.
The middleware stays focused; the pipeline is the orchestration. It's a better model
than the old `HttpModule` lifecycle, and the test I can actually write seals it.
