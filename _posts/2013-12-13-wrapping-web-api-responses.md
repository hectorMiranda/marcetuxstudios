---
layout: post
title: "Wrapping Web API responses in a consistent envelope"
date: 2013-12-13
author: marcetux
tags: [dotnet, webapi, rest, api, architecture]
---
The v2 API surface gave me the opportunity to fix something I'd been living with in v1:
inconsistent response shapes. A successful `GET` returns the resource directly; a failed
`GET` returns a string error message; a validation failure returns the `ModelState`
dictionary. Three different shapes for callers to handle depending on what happened.

The consistent envelope pattern wraps every response in a common structure: `{ "success":
true, "data": {...} }` for success and `{ "success": false, "errors": [...] }` for
failure. Every endpoint returns the same outer shape; the `data` field carries the
resource or null; the `errors` field carries a list of structured error objects with
a code, a message, and optionally a field name for validation errors.

The implementation is an `IHttpActionResult` wrapper class that the controller actions
return, and an exception filter that catches unhandled errors and wraps them in the same
envelope with an appropriate status code. From the caller's perspective, every response
from the v2 API is deserializable into the same C# or JavaScript type, and the branch
on `success` is the only conditional they need. I'm not a religious adherent of any
particular envelope convention; I'm an adherent of having *one* envelope consistently.
The inconsistency in v1 is what makes the client code brittle — the shape is the
contract, and changing it is breaking the contract.
