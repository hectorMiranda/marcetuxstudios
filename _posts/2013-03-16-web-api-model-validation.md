---
layout: post
title: "Web API model validation done once at the boundary"
date: 2013-03-16
author: marcetux
tags: [dotnet, webapi, validation, rest, architecture]
---
The Web API controllers for the portal's REST surface had validation scattered
everywhere — some in the controller action, some in a service layer, some just missing.
When a caller sent a bad payload they might get a 500, a meaningful 400, or sometimes
silence. Consolidating it into one place was this sprint's cleanup story.

The answer in Web API is `ModelState`. The framework deserializes the incoming JSON and
runs DataAnnotations (`[Required]`, `[Range]`, `[StringLength]`) on the model class.
Then a filter — a single class registered once at startup — checks `ModelState.IsValid`
and returns a `400 Bad Request` with the validation errors serialized if it isn't. Every
controller action that receives a model gets that check for free, without a single `if
(!ModelState.IsValid)` inside the action body.

The contract gets cleaner too: the model class is now the authoritative definition of
what the API expects. A teammate reading the model knows the constraints without
hunting through service code. Validation belongs at the boundary where the data enters
the system; everything inside the boundary can assume the data is valid. I should have
set this up when we started Web API, not six months later.
