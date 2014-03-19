---
layout: post
title: "Content negotiation in Web API"
date: 2014-03-18
author: marcetux
tags: [dotnet, webapi, rest, http, api]
---
A partner integration needed XML responses from the same endpoints that return JSON
to browsers. My first instinct was to add an `?format=xml` query parameter and branch
in the controller, which is exactly the wrong way to do it. Web API has content
negotiation built in; I just hadn't used it intentionally before.

Content negotiation means the server looks at the request's `Accept` header and picks
the best representation it can produce. A browser sending `Accept: application/json`
gets JSON; an integration client sending `Accept: application/xml` gets XML. The
controller returns an object — it doesn't pick the format. The pipeline inspects the
`Accept` header and invokes the right formatter. Web API ships with JSON and XML
formatters registered by default; the `JsonMediaTypeFormatter` handles JSON with
Json.NET, and the `XmlMediaTypeFormatter` handles XML.

The one thing I had to tune was the XML formatter's default behavior. By default it
serializes with the `DataContractSerializer`, which adds namespaces and wraps things in
ways the partner didn't want. Switching to the `XmlSerializer` and controlling the
output with `[XmlElement]` attributes gave clean XML that matched the partner's
expected schema. The controller stayed the same for both formats — which is the right
outcome. Format selection belongs in the pipeline, not in the business logic.
