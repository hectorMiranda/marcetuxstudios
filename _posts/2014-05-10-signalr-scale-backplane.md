---
layout: post
title: "Scaling SignalR beyond one server"
date: 2014-05-10
author: marcetux
tags: [dotnet, signalr, realtime, redis, scaling]
---
The two-server setup from the Redis backplane work in April is running in QA, and the
load test revealed a subtlety I'd missed: the Redis backplane solves the fan-out problem
but not the sticky-session problem. A browser WebSocket connection is stateful — it
belongs to a specific server process — and if the load balancer sends subsequent HTTP
requests to a different server, the SignalR infrastructure on that server doesn't have
the connection. The symptom is silent: the connection appears healthy from the browser,
messages just stop arriving.

The fix is sticky sessions at the load balancer: once a browser establishes a connection
to a server, route that browser's requests to the same server for the lifetime of the
connection. Most load balancers do this with a cookie. The load balancer writes a cookie
on the first response; subsequent requests carry the cookie; the load balancer routes by
cookie value. For SignalR that's enough — the WebSocket is persistent, the sticky session
keeps HTTP upgrade requests on the same box.

For long-poll fallback (which some proxies force), sticky sessions are even more critical
— every poll request must hit the same server. The checklist for SignalR scale: Redis
backplane for fan-out, sticky sessions for connection affinity, health checks on the
backplane connection so a Redis restart doesn't silently break all pushes. Get all three
wrong and you get intermittent failures that are miserable to debug.
