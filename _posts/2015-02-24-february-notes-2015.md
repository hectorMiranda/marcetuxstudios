---
layout: post
title: "February notes, 2015"
date: 2015-02-24
author: marcetux
tags: [meta, retrospective]
---
February was denser than expected. The Docker compose setup for local dev is in use now
— two new people onboarded this month and the "it just works" ratio was much higher than
the wiki-page era. Rails models are getting service object surgery; the match score
calculator was the first extraction, and the tests already run faster.

The home net gained a second ESP8266 node this week — door sensor on the front entrance,
publishing state changes to Mosquitto. The broker is stable, the retained topics survive
reconnects, and now I want InfluxDB + a small dashboard. That's March's project if work
doesn't eat the weekends.

HTTP/2 being finalized changes some assumptions about how we build asset pipelines and
whether domain sharding is even a sensible strategy going forward. Angular 2 alpha is
architecturally coherent but the migration from 1.x is essentially a rewrite, which is
the polite way of saying it's a new framework with a familiar name. Both things to file
and revisit when they're not alpha.
