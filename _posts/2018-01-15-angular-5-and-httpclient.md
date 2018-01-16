---
layout: post
title: "Angular 5 and the new HttpClient"
date: 2018-01-15
author: marcetux
tags: [angular, typescript, frontend, http, javascript]
---
The dashboard upgrade from Angular 4 to 5 was fast — the CLI's `update` command
handled most of the mechanical bits — but the piece worth writing up is the new
`HttpClient` module that shipped with 4.3 and is now the blessed way to talk to APIs.
The old `Http` service is still there but officially deprecated, and the replacement
is better enough that moving immediately is the right call.

The surface area improvement is real: response bodies are typed by default, so
`http.get<Patient[]>('/api/patients')` returns an `Observable<Patient[]>` the compiler
actually knows about. Interceptors are first-class — request headers, auth tokens,
and retry logic all live in one place instead of scattered across a base service class
I'd hacked together. Progress events for file uploads come for free. The response
pipe is just the RxJS pipe you already use for everything else.

The migration path is a find-and-replace on the import plus a module swap, then a
light touch wherever you'd been `.map(r => r.json())` to unwrap the old response
wrapper — the new one gives you the parsed body directly. An hour of work for a
notably cleaner HTTP layer. The Angular team has been more conservative about
breaking changes than I expected from a v5 release cycle, and that credibility is
paying off.
