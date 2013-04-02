---
layout: post
title: "OAuth2 server-side flow without the magic"
date: 2013-04-01
author: marcetux
tags: [oauth2, security, dotnet, webapi, authentication]
---
I deferred the OAuth2 integration twice because the diagrams looked scarier than they
needed to. When I finally sat down with the spec and walked through the Authorization
Code flow step by step, the complexity collapsed — it's four HTTP calls and a lot of
careful bookkeeping.

The server-side flow goes: browser gets redirected to the authorization server with a
`client_id`, a `redirect_uri`, and a random `state` value I generate and store in
session. The user authenticates there, consents, and gets bounced back to my
`redirect_uri` with a short-lived `code` and the same `state`. I verify the `state`
matches what I stored — that's the CSRF protection — then swap the `code` for an
`access_token` in a server-to-server POST using the `client_secret`. The browser never
sees the secret, and the `access_token` never appears in the URL where a proxy might
log it.

The implementation in Web API is a handful of controller actions: one initiates the
redirect, one handles the callback, one middleware registers the token on the
`IPrincipal`. What made it click was doing it without a library first — just `HttpClient`
and a couple of string checks. Libraries are fine once you understand what they're
doing. Understanding it first means you know which part broke when it breaks.
