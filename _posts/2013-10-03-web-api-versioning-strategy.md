---
layout: post
title: "Web API versioning without breaking existing callers"
date: 2013-10-03
author: marcetux
tags: [dotnet, webapi, rest, api, architecture]
---
The reporting API needs a v2 with a different response shape — the existing shape was
designed for the original dashboard and doesn't fit the new partner integration without
some gymnastics. Three teams are calling v1. The question is how to evolve the API
without breaking them.

The common options are URL versioning (`/api/v1/`, `/api/v2/`), header versioning
(`Accept: application/vnd.myapp.v2+json`), and query string versioning (`?version=2`).
URL versioning is the most visible — it's in every log line, every cached URL, every
partner's code. Header versioning is "correct" per REST purists and invisible to every
monitoring tool. After arguing this with myself for a week, I went with URL versioning
for a practical reason: it's trivially cacheable at the CDN edge and trivially
debuggable in a Fiddler trace. The theoretical cleanliness of header versioning isn't
worth the operational opacity.

The implementation is a route prefix convention: all v1 controllers live in a `V1`
namespace and carry `[RoutePrefix("api/v1")]`; v2 controllers carry `api/v2`. Shared
business logic lives in the service layer, which both versions call. The v1 and v2
response model classes are separate even when the underlying data is the same —
the mapping between the service result and the response shape belongs in the controller,
not in the domain model. Three teams can stay on v1 indefinitely; the v2 partner
gets the shape they need.
