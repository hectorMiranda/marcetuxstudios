---
layout: post
title: "Letting the Accept header pick JSON or XML"
date: 2012-09-12
author: marcetux
tags: [rest, api, json, xml, http]
---

A short one, because it's a small idea that saves a lot of arguing: stop putting
the format in the URL.

I still see APIs with `/report.json` and `/report.xml` as separate endpoints, or
worse, a `?format=xml` query parameter. HTTP already has a mechanism for this. The
client says what it wants in the `Accept` header, the server honors it, and you
maintain **one** resource at **one** URL.

`Accept: application/json` → JSON. `Accept: application/xml` → XML. Same data, same
route, the serializer chosen by content negotiation. Our integrators who live in
enterprise XML land and the ones building JavaScript dashboards hit the identical
endpoint and each get what they asked for.

It also keeps your URLs honest. A URL should name a *thing*, not a *representation
of a thing*. The representation is a detail you negotiate at request time.

The one place I'll bend this: a "download as CSV" link a human clicks in a browser.
Browsers send a vague `Accept`, so an explicit `?format=csv` for that button is a
pragmatic exception, not the rule.
