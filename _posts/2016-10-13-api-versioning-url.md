---
layout: post
title: "URL versioning for APIs is boring and I am at peace with that"
date: 2016-10-13
author: marcetux
tags: [api, rest, architecture, versioning, backend]
---
The transcoding status API I ported to .NET Core got a second endpoint this month, which
meant I had to actually decide on a versioning strategy rather than leaving it implicit.
The options are: URL versioning (`/v1/jobs/{id}`), header versioning (`Accept:
application/vnd.jibjab.v1+json`), and query parameter versioning (`?version=1`). I've
been through this debate before and I keep landing in the same place.

URL versioning is the right default for most cases and the main arguments against it
don't survive contact with reality. "It's not RESTful because a resource should have
one URL" — true in a purist sense and irrelevant in practice, because clients can
bookmark and cache the version-tagged URL exactly as well as the versionless one.
"Header versioning is more elegant" — it is, in the same way code without newlines is
more concise: technically correct, harder to work with. You can't test a header-versioned
API from a browser address bar or curl without extra flags.

The practical case for URL versioning: it shows up in logs, in metrics, in every HTTP
toolkit without configuration. When I look at the access logs and see `/v1/jobs` and
`/v2/jobs` traffic I know exactly who's on what version. When I want to deprecate v1,
I write an access-log query, send an email, and watch the traffic migrate. Boring
machinery, unambiguous information. The sophisticated option usually has a reason it's
not the default.
