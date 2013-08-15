---
layout: post
title: "CORS preflight requests and getting Web API to behave"
date: 2013-08-14
author: marcetux
tags: [http, cors, webapi, security, rest]
---
A partner built a browser app that calls our API from a different origin, and the
first thing they reported was that `GET /api/v1/customers` worked fine but `PUT
/api/v1/customers/5` returned nothing — the browser just swallowed the request. The
cause is the same-origin policy and preflight requests, and the fix is wiring CORS
headers correctly into Web API.

Browsers enforce the same-origin policy: JavaScript can only make certain requests to
the same origin the page came from. For "simple" requests — `GET` and `POST` with
standard headers — the browser just makes the request. For anything else — `PUT`,
`DELETE`, custom headers, JSON content type — the browser first sends an `OPTIONS`
preflight asking the server what it allows. If the server doesn't respond to the
`OPTIONS` with the right `Access-Control-Allow-*` headers, the browser treats the
subsequent real request as blocked, and the partner sees silence.

Web API 2 ships a CORS package (`Microsoft.AspNet.WebApi.Cors`) that handles this with
an attribute: `[EnableCors(origins: "https://partner.example.com", headers: "*",
methods: "GET,PUT,DELETE")]` on the controller, or globally in `WebApiConfig`. The
package generates the correct response headers including `Access-Control-Allow-Origin`
and responds to `OPTIONS` preflights with the allowed methods and headers. The
`origins` parameter should name specific domains, not `*`, for any endpoint that uses
cookies or HMAC auth — a wildcard origin is fine for truly public, unauthenticated
data.
