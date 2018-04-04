---
layout: post
title: "API versioning strategies and why URL versioning wins"
date: 2018-04-03
author: marcetux
tags: [api, rest, architecture, backend, aspnet]
---
We're at the point where the mobile app has two versions in the wild and I'm adding
a field that the old version will choke on if it receives it. That's the moment API
versioning stops being theoretical. We evaluated the three standard approaches — URL
path (`/v2/`), query string (`?version=2`), and custom header — and went with URL
path, which is the choice I'll make again.

The practical argument for URL versioning is caching and debuggability. A versioned
URL is a distinct resource; you can inspect it in a browser, paste it into Postman
without configuring header presets, cache it at the CDN layer without custom vary
rules, and reverse-proxy it with ordinary routing rules. Content negotiation via a
custom header is theoretically purer — a resource is a resource, a version is a
representation — but theory doesn't show up on the latency graph. URL versioning
also makes logging readable: the version is in every access log line without any
parsing.

The discipline is maintaining both versions in parallel until the old clients retire.
ASP.NET's `Microsoft.AspNetCore.Mvc.Versioning` package makes this mechanical: decorate
controllers with `[ApiVersion("1.0")]` and `[ApiVersion("2.0")]`, register the reader,
and the routing handles dispatch. The real work is not the routing — it's writing the
migration guide that lets clients move on a schedule rather than waiting for a push
notification that you pulled v1 without warning them.
