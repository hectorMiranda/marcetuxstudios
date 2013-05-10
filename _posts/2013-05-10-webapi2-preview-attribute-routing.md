---
layout: post
title: "Web API 2 preview and attribute routing"
date: 2013-05-10
author: marcetux
tags: [dotnet, webapi, rest, routing, aspnet]
---
The Web API 2 preview shipped quietly as part of the ASP.NET nightly builds, and the
headline feature is something I've wanted since we started the REST surface: attribute
routing. Instead of defining every route in a central `WebApiConfig.Register` table,
you put a `[Route("customers/{id}/bandwidth")]` attribute directly on the controller
action where it belongs.

The old convention routing is fine for flat resources — `GET /api/customers/5` maps
naturally to a controller named `CustomersController`, action `Get`. But our API has
nested and action-style routes that convention routing can't express cleanly without
either creative controller naming or a tangle of route table entries with optional
segments. Attribute routing expresses the URL shape where the code lives, which means
reading the controller is enough to know the API surface.

I'm not flipping a production controller to a preview API today, but I've built the
new reporting endpoint against the preview bits because it's greenfield and the
attribute routing makes the route hierarchy legible at a glance. The concern I keep
hearing is "magic" — that attribute routing hides the routing in the markup and you
lose a single place to audit routes. My honest counter is that the single place was
always more comforting than useful; the action and its route are the thing you want
co-located.
