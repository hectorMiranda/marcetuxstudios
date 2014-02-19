---
layout: post
title: "Web API exception filters and consistent error responses"
date: 2014-02-18
author: marcetux
tags: [dotnet, webapi, rest, errorhandling, api]
---
A customer integration complained last week that when our API fails, the error format
is different every time. Sometimes it's a serialized exception object with an inner
stack trace in it — which is both an information leak and useless to the caller.
Sometimes it's a 200 with an error message in the body, which is worse. The root cause
is that each controller was handling failures differently, or not at all.

Web API's `ExceptionFilterAttribute` is the single place to intercept all unhandled
exceptions before they leave the pipeline. The filter inspects the exception type,
maps it to an HTTP status code — `NotFoundException` becomes 404, `ValidationException`
becomes 400, anything else becomes 500 — and writes a consistent JSON error body with a
code, a message, and a correlation ID that ties the request to the server logs. The
stack trace stays server-side.

The payoff is immediate: callers get a contract they can program against, the support
team has a correlation ID to look up instead of asking for a reproduction case, and
the security team stops getting nervous about stack traces leaving the building. One
filter on the `HttpConfiguration`, and every controller inherits the policy. Push the
error-shaping to where it belongs — at the edge of the process — and never think about
it in a controller again.
