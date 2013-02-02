---
layout: post
title: "OWIN and Katana, a second look"
date: 2013-02-01
author: marcetux
tags: [dotnet, owin, katana, aspnet, architecture]
---
I poked at OWIN back in December and called it promising-but-early. February's
resolution was to figure out where it's actually heading, so I spent a couple of
evenings with Katana — Microsoft's implementation of the OWIN spec — and now I get
the point in a way the abstract spec didn't convey.

The whole idea is decoupling your web app from `System.Web` and IIS. OWIN is just a
contract — a dictionary of the request environment and a pipeline of middleware
functions that pass it along. The payoff is that your app stops being welded to the
host. The same code can run under IIS, or self-host in a console process, or in a
test harness, because none of it reaches into `HttpContext` anymore.

What sold me is the middleware pipeline. Authentication, logging, compression — each
becomes a small piece you add to the pipe in order, instead of a tangle of
`HttpModules` configured in `web.config`. It's the Express/Rack model arriving in
.NET, and it's the right model. Still early — the tooling is rough and half the
samples are self-hosting toys — but this is the direction, and I'd rather learn it
now than in a panic in two years.
