---
layout: post
title: "The OWIN authentication middleware pipeline in practice"
date: 2013-07-05
author: marcetux
tags: [dotnet, owin, authentication, katana, security]
---
Wiring the OAuth2 refresh into the OWIN pipeline forced me to understand how Katana
structures authentication at the middleware level, and it's cleaner than the old
Forms/Windows authentication `HttpModule` world. The key concept is the
`AuthenticationManager` that every OWIN request context carries: middleware reads
from it, writes to it, and the application code asks it questions without knowing what
put the answer there.

Authentication middleware runs early in the pipeline. It inspects the request — a
cookie, a bearer token, an external provider callback — and if it can construct an
identity, it calls `context.Authentication.SignIn(identity)`, which attaches a
`ClaimsPrincipal` to the request. The Web API layer downstream just reads
`RequestContext.Principal` and doesn't care whether that principal was established by
a cookie, a JWT, or an OAuth2 token — the middleware abstracted it.

The practical outcome for us: the cookie middleware handles session auth for the portal
UI; a bearer token middleware handles the API clients. Both register in `Startup.cs`
in pipeline order. A request to an API endpoint with a bearer token skips the cookie
middleware and hits the bearer middleware, which populates the principal. The API
endpoint gets a properly authenticated caller. Adding a third auth scheme later means
adding a third middleware, not forking the authentication logic.
