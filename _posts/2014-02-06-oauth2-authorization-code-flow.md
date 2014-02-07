---
layout: post
title: "OAuth2 authorization code flow, from scratch"
date: 2014-02-06
author: marcetux
tags: [oauth2, security, rest, dotnet, api]
---
I knew OAuth2 well enough to use it — configure the middleware, wire the callback route,
get a token — but when a partner integration required a custom authorization server I
had to actually understand it. Walking through the authorization code flow from first
principles took a day and was worth every minute.

The code flow has a clear sequence: client redirects the browser to the authorization
server with a client ID and a requested scope; user authenticates and approves; server
redirects back with a one-time authorization code; client exchanges the code for an
access token from the server's token endpoint, this time with a client secret over a
back-channel. The access token never touches the browser's address bar. That exchange
step — code for token, on the server, with a secret the browser never sees — is the
part that actually provides security. A lot of implementations treat it like boilerplate
and miss why it matters.

Building it in Web API meant a custom `IAuthorizationCodeProvider` and a token endpoint
that validates the code, checks expiry, and issues a JWT. The JWT part was the fun
piece — signing the token with RSA so any service can verify it without calling home.
I'd been using symmetric tokens everywhere; the asymmetric model makes more sense at
scale, and I understand why now.
