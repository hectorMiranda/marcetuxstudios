---
layout: post
title: "RAML and design-first API workflow"
date: 2018-07-05
author: marcetux
tags: [raml, api, mulesoft, architecture, documentation]
---
The Mulesoft ecosystem uses RAML — RESTful API Modeling Language — the way the rest
of the world uses OpenAPI, and after a month of fighting it I've come around to it
as a design-first workflow tool even when the runtime isn't Mulesoft. RAML describes
resources, methods, request/response types, and examples in a YAML dialect that's more
terse than OpenAPI 3.0 and considerably more readable than what Swagger 2.0 produced.

The design-first discipline is the part I keep returning to. When you write the RAML
document before writing the implementation, the review conversation changes. Stakeholders
and consumers look at a document that describes the API as a system — not a code PR
they'd skip reviewing. The gaps in the design show up in words and examples, which are
easier to argue about than in method signatures. The discipline the format enforces
is: make every decision explicitly before you implement it, because changing the API
after clients depend on it is expensive.

What I don't like: the tooling ecosystem is thinner than OpenAPI's. The community
around Swagger UI, Redoc, and code generation tools is larger and more maintained.
For a pure Mulesoft shop, RAML is the right choice. For anything that needs to live
outside Anypoint Studio, I'd still reach for OpenAPI 3.0. The design-first practice
transfers; the format choice depends on what's reading the file at the other end.
