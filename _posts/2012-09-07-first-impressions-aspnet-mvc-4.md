---
layout: post
title: "First impressions of ASP.NET MVC 4"
date: 2012-09-07
author: marcetux
tags: [csharp, aspnet, mvc, webapi]
---

Spent the week with ASP.NET MVC 4 now that it's shipped, and the headline for me
isn't the MVC bits — it's **Web API** riding in the same box.

For years I've built HTTP endpoints by bending WCF or hand-rolling handlers. Web
API finally treats "return an object, let the framework negotiate the format" as
the default. Same controller, and the response comes back as JSON or XML based on
the `Accept` header without me writing a serializer per route.

Other things I liked: bundling and minification out of the box (no more hand-
maintained script tags), and the mobile-friendly project template that actually
acknowledges phones exist in 2012.

What I'm watching warily: it's easy to end up with two parallel stacks — MVC
controllers and API controllers — that don't share as much as you'd hope. For a
reporting portal where the page and its data API live side by side, I want one
mental model, not two. Still figuring out where to draw that line.
