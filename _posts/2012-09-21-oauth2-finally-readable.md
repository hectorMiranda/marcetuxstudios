---
layout: post
title: "OAuth 2.0, finally readable"
date: 2012-09-21
author: marcetux
tags: [security, oauth, api, http]
---

OAuth 2.0 went to RFC last year and I've now read it closely enough to stop being
intimidated by it. The big relief: 2.0 dropped the request-signing ceremony that
made 1.0a such a slog, and leaned on **TLS plus bearer tokens** instead.

The mental model that made it click: OAuth is about *delegated* access. A user lets
an app act on their behalf without handing over their password. The app gets a
**token**, not credentials, and the token is scoped and expirable.

For the integrations I build, the **authorization code** flow is the one that
matters: redirect the user to the provider, they approve, you get a short-lived
code on the callback, and you exchange that code server-side for an access token.
The client secret never touches the browser.

The part teams get wrong is treating the access token like a password to store
forever. It isn't. It's short-lived on purpose; you keep the **refresh token**
safe and mint new access tokens as needed. And because 2.0 leans entirely on TLS
for confidentiality, "we'll add HTTPS later" is not an option here — the transport
*is* the security.
