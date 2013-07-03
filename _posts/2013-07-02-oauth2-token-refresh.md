---
layout: post
title: "OAuth2 token refresh and not kicking users out every hour"
date: 2013-07-02
author: marcetux
tags: [oauth2, security, dotnet, webapi, authentication]
---
The access token we get from the authorization server expires in sixty minutes. For
the past two months the behavior on expiry has been: the next API call returns a `401`,
Angular's HTTP interceptor catches it, and the user gets redirected to the login page.
This is technically correct and operationally annoying — losing a session mid-report
doesn't make anyone happy. The fix is the refresh token flow, and I finally wired it
in this week.

The authorization server issues a `refresh_token` alongside the `access_token` at the
end of the initial flow. The refresh token is longer-lived and can be exchanged — via a
server-to-server `POST`, using the `client_secret` that never goes to the browser —
for a new `access_token` without user interaction. My Web API's OAuth middleware stores
the refresh token in session, and a `DelegatingHandler` in the request pipeline checks
the token's expiry time before each outbound call to the upstream API; if it's within
five minutes of expiry, it exchanges the refresh token silently, updates the session,
and proceeds.

The security discipline: refresh tokens are long-lived and must be stored as carefully
as the `client_secret`. Losing one to a session fixation attack or a server-side
session leak is equivalent to losing persistent access. Storing them in the server-side
Redis session (not in a cookie or local storage) is the minimum acceptable approach. The
convenience for users comes from doing the right thing securely, not from doing the easy
thing.
