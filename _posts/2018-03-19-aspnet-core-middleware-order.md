---
layout: post
title: "Middleware order in ASP.NET Core matters more than you think"
date: 2018-03-19
author: marcetux
tags: [aspnet, dotnet, csharp, architecture, backend]
---
A new hire put the CORS middleware after the routing middleware in the pipeline and
spent an afternoon puzzling over why preflight requests were failing on some endpoints
but not others. The answer is that ASP.NET Core's middleware is not declarative — it's
a literal pipeline, and order is execution order. If CORS runs after routing, the
routing middleware has already short-circuited some requests before CORS gets a turn.

The mental model is a chain of delegates, each one calling `next()` to pass the request
downstream. Middleware registered first runs outermost — first on the way in, last on
the way out. That means exception handling should be first (to catch anything from
further down), then HTTPS redirect, then HSTS, then static files, then routing, then
authentication, then authorization, then the endpoint handlers. The framework docs
diagram this correctly; the issue is that it looks like configuration when it's
actually code with a real execution order.

I made a quick reference card for the team showing which middleware must precede
which, with a one-sentence "why" for each constraint. The rule I emphasize most: if
a middleware reads something set by another middleware — authentication reading a
routing token, for example — it must come *after* that other middleware in the
pipeline. The constraint is always a data dependency, and making it explicit is
faster than debugging a mysterious 401.
