---
layout: post
title: ".NET 6 Minimal APIs preview: what changes"
date: 2021-07-05
author: marcetux
tags: [dotnet, aspnet, csharp, architecture]
---
The .NET 6 previews have been shipping monthly and the Minimal APIs feature is the
one I keep coming back to. The pitch: define an HTTP endpoint in one line without
a controller class, a Startup class, a Configure method, or any of the ceremony
that MVC inherited from ASP.NET Web Forms via ASP.NET MVC via two decades of
backwards compatibility. `app.MapGet("/health", () => "ok")` and you have an
endpoint.

I set up a preview prototype to see where the friction actually lives. For a small
service — a dozen endpoints, simple request/response shapes — the reduction in
structural noise is real. There's no controller class to navigate to, no action
name to remember, no attribute routing syntax to look up. The handler is a lambda
or a named function right at the registration site. The routing, the HTTP method,
the handler — they're colocated and readable in sequence.

The honest limits: the implicit DI injection works well for services but gets
awkward when the handler has complex validation or needs middleware that assumes
controller conventions. And the discovery story — "what endpoints does this service
have?" — that MVC's controller+action pattern makes obvious gets murkier when
endpoints are wired up in arbitrary order in `Program.cs`. I'd reach for Minimal
APIs on new internal microservices where the team controls both sides. For a large
public API surface where discoverability and versioning matter, controllers still
earn their ceremony.
