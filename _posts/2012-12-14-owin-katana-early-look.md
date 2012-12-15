---
layout: post
title: "An early look at OWIN"
date: 2012-12-14
author: marcetux
tags: [dotnet, owin, web, architecture]
---
Poked at OWIN this week and I think it's a bigger deal than it looks. The pitch: a
standard interface between .NET web servers and applications, so your app isn't
welded to System.Web and IIS.

Today "an ASP.NET app" basically means "an IIS app" — the framework and the server
are one lump. OWIN defines a simple contract (an app is a function over a
dictionary of environment data) and a middleware pipeline, so you can compose the
request pipeline yourself and host the same app on IIS, or self-host it in a
console app or a Windows service.

It's early — the Katana implementation is young and the ecosystem is thin. But the
direction is unmistakable: decouple the framework from the host, make middleware
composable, get System.Web off the critical path. I don't know exactly what this
turns into, but I'd bet the next generation of .NET web stacks is built on this
idea. Worth watching closely.
