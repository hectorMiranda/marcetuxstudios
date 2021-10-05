---
layout: post
title: ".NET 6 RC and Minimal APIs closer to production"
date: 2021-10-04
author: marcetux
tags: [dotnet, aspnet, csharp, architecture]
---
.NET 6 RC1 landed and I've been running the Minimal APIs prototype in a more
realistic configuration: middleware pipeline, authentication, endpoint filters as
the replacement for action filters, and a FluentValidation integration. The RC is
stable enough that the prototype doesn't feel like prototype work anymore — it
feels like a decision.

The endpoint filters in the RC are the piece that was missing in the preview. An
action filter in MVC gave you a before/after hook around a handler without the
handler knowing about it; endpoint filters in Minimal APIs do the same thing with
a functional signature. `IEndpointFilter.InvokeAsync` takes the context and the
next delegate — it's the middleware pipeline model applied at the route level. The
pattern is unfamiliar compared to `[ActionFilter]` attributes but more composable:
you can add filters in a chain programmatically, which lets a shared library
provide a logging-or-validation filter without requiring attribute usage.

The thing I've made peace with is that Minimal APIs and MVC are not competing for
the same use cases. Minimal APIs fit new microservices where you control the
entire surface area and want minimum ceremony. MVC fits applications where you
need the full scaffolding story, the rich routing attributes, the area-based
organization of a large team. Holding both in the toolbox — and being deliberate
about which you reach for — is the correct posture. Greenfield internal service
next month goes Minimal.
