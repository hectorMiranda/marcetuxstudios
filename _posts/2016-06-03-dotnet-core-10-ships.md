---
layout: post
title: ".NET Core 1.0 ships and the tooling story is almost there"
date: 2016-06-03
author: marcetux
tags: [dotnet, dotnet-core, csharp, backend, linux]
---
.NET Core 1.0 hit general availability this week — June 27 is the official date but the
bits have been live for a couple of days — and after eighteen months of RCs and previews
it's a real thing you can deploy to production. I spent the week after the announcement
porting a small internal API service to Core and running it on a Linux container. It
works. The experience is not entirely smooth, but "not entirely smooth" is miles ahead
of "doesn't run on Linux at all."

The runtime itself is solid. ASP.NET Core's middleware pipeline is what OWIN was
pointing at in 2013 — clean composition, no `System.Web` dependency, startup
configuration that makes sense. Kestrel runs fast on Linux. The thing I keep bumping
into is the project tooling: `project.json` is the build format right now, with the
promise of migrating to `.csproj` eventually. That means any tooling investment in the
`project.json` world has a known expiration date. Not a dealbreaker, just a thing to
track.

My read: .NET Core 1.0 is the right foundation for new backend services that need to
run on Linux or in containers. I wouldn't port a large existing ASP.NET 4.x app today
— the API surface differences are real and the ecosystem hasn't caught up — but greenfield
on Core is a defensible choice. The platform story Microsoft has been building toward
since 2014 is here. It just needs one more release cycle of tooling polish.
