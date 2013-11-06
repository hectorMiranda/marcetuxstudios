---
layout: post
title: "Redis Lua scripting for atomic token operations"
date: 2013-11-05
author: marcetux
tags: [redis, lua, dotnet, security, caching]
---
The OAuth2 refresh token exchange has a subtle race: if two requests arrive within
milliseconds of each other, both see a near-expiry token, both try to exchange the
refresh token, the first exchange succeeds, and the second exchange may fail because
the authorization server marks refresh tokens as single-use. The session ends up in
an inconsistent state while one request completes and one returns a 401.

Redis Lua scripts run atomically — no other Redis command can interleave with a script's
execution. I replaced the three-step "check expiry, load token, compare-and-swap new
token" sequence with a single Lua script that does all three steps atomically. The
script checks whether the stored access token is expired, and if so, sets a "refresh
in progress" flag and returns the refresh token to the caller. The caller exchanges it,
gets a new access token, and calls a second script to store the new token and clear the
flag. Any concurrent request that hits the "refresh in progress" flag waits briefly and
retries rather than initiating a duplicate exchange.

The implementation is `StackExchange.Redis.IDatabase.ScriptEvaluate()` with the Lua
source loaded as a string. The Lua script lives in a `.lua` file in the project and
is read at startup — no string literals spread through C# code. Atomicity at the Redis
layer means the application doesn't need a distributed lock for this specific operation.
That's the composable property of Redis scripts: they're the lock for the one operation
they represent, not a general-purpose lock mechanism.
