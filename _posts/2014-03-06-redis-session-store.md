---
layout: post
title: "Redis as a session store, replacing SQL"
date: 2014-03-06
author: marcetux
tags: [redis, session, performance, dotnet, caching]
---
The portal's session data has been in SQL Server since launch, which is fine for one
web server and slow for two. When we added a second server for redundancy, the session
table became the thing every request hit and no server owned. Moving to Redis fixed
both the performance problem and the scaling problem at the same time, which is the
kind of refactor that earns its keep immediately.

Redis is a key-value store that lives entirely in memory, which makes reading a session
a sub-millisecond operation versus the SQL round-trip. The integration is a NuGet
package that swaps the session provider; the application code doesn't know or care
where the session data comes from. Set an expiry on the key and Redis handles eviction
automatically — no cleanup job needed, unlike the SQL-based approach that accumulated
stale sessions until a scheduled proc cleared them.

The one thing to plan for is Redis restart. An in-memory store that loses data on
restart would wipe all active sessions — logged-out users everywhere. Redis persistence
options (RDB snapshotting, AOF logging) mitigate this, and for session data where a
re-login is acceptable I went with RDB snapshots every few minutes. Sessions are not
critical data. The rule I use: if losing it would inconvenience a user but not corrupt
anything, Redis is the right store; if it would corrupt something, it belongs in a
durable database.
