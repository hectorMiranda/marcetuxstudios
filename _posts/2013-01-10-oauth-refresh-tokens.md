---
layout: post
title: "Refresh tokens in practice"
date: 2013-01-10
author: marcetux
tags: [security, oauth, api]
---
Follow-up to the OAuth post, because the access-token-vs-refresh-token distinction
is where real integrations live or die. Short-lived access tokens are the security
win; refresh tokens are the operational reality that makes short-lived tokens
bearable.

The flow in practice: access token expires in an hour, and instead of dragging the
user back through a login, the client quietly exchanges its refresh token for a new
access token. The user never notices. The refresh token is the crown jewel — it
lives server-side, encrypted, never in a browser, and if it leaks you revoke it and
the damage is bounded.

The bug I keep seeing (and just fixed): clients that wait for a `401`, then refresh,
then retry — but don't handle two requests refreshing *at once* and racing. The
second refresh invalidates the first's new token and you get a logout storm.
Serialize the refresh: one in flight, everyone else waits for its result. Concurrency
is where token handling actually gets hard.
