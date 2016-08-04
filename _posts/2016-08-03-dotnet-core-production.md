---
layout: post
title: ".NET Core 1.0 on a production API endpoint"
date: 2016-08-03
author: marcetux
tags: [dotnet, dotnet-core, csharp, linux, devops]
---
The internal transcoding status API — the thing the front end polls to show job progress
— was a tiny ASP.NET 4.6 service running on a Windows instance. It was the smallest
possible excuse to try .NET Core 1.0 in production: low traffic, zero critical-path
consequences, and the API surface is trivial enough that the port took one afternoon. It
runs on Linux in a Docker container on an EC2 instance now, and it has been running that
way for two weeks without incident.

The port was straightforward where the surface was modern — the controller/action model
transferred cleanly, JSON serialization via `Newtonsoft.Json` works identically, the DI
container is familiar. The friction came from a few APIs that were either missing or
significantly changed: no `HttpContext.Current` (expected), different configuration
system (`appsettings.json` instead of `web.config`, which is actually better), and a
handful of NuGet packages that haven't published a `netstandard` build yet. The
ecosystem gap is real and proportional to how niche your dependency list is.

Two weeks of production without incident is not a data point about reliability, it's a
data point about "this works well enough to iterate on." The thing I trust is that the
platform is aligned — Microsoft is investing heavily in Core and the ecosystem will
follow. Getting a service on it now means I understand the gaps rather than being
surprised by them at scale.
