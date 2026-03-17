---
layout: post
title: "Token refresh and session design without cutting corners"
date: 2026-03-16
author: marcetux
tags: [identity, jwt, auth0, sessions, security]
---
Short-lived access tokens are the right choice, and the friction that makes teams resist them — "the user keeps getting logged out" — is almost always a refresh implementation problem rather than a token lifetime problem. When the refresh is invisible and seamless, users don't notice a 15-minute access token any more than they notice a DNS TTL. When the refresh is broken or absent, teams lengthen the lifetime as a workaround and call it done, which trades a user experience problem for a security posture problem.

The refresh flow with Auth0 is standard: the client holds a refresh token with a longer lifetime and a rotation policy, and when the access token approaches expiry, it exchanges the refresh token for a new access token and a new refresh token in one call. The key implementation detail is that this exchange has to happen *before* the access token expires, not after. A client that only refreshes on a 401 response will occasionally serve a stale-token request to a user who's actively working, which is the logout experience people complain about. The right trigger is a timer or an expiry check before the request goes out.

On the server side, Auth0's refresh token rotation gives each refresh token a single use — once exchanged, the old token is invalid. This is the right default because it means a stolen refresh token can only be used once before the legitimate client's next refresh detects the conflict (both sides try to use a token that's already been consumed). The consequence is that clients have to persist the new refresh token after every exchange, which sounds obvious but is the step that browser SPA clients often miss, leaving the session backed by a token that was already rotated away.
