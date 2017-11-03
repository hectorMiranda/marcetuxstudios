---
layout: post
title: "Angular 5 in production and the HttpClient migration we needed anyway"
date: 2017-11-02
author: marcetux
tags: [angular, typescript, frontend, upgrade]
---
Angular 5 released in early November and the upgrade went smoothly, which is partly
credit to the Angular team and partly credit to staying current with the RCs. The one
piece that required real work: the migration from the deprecated `Http` module to
`HttpClient`. I'd been deferring it in the Go RN codebase, and Angular 5's final
deprecation notice was the right forcing function.

`HttpClient` is better in ways that matter for the app. The response type is generic —
`this.http.get<Shift[]>(url)` returns an `Observable<Shift[]>`, which means TypeScript
can catch type mismatches between the API shape and the model. The old `Http` returned
`Observable<Response>` and the JSON parsing was manual; with `HttpClient` it's
automatic. Request and response interceptors are first-class, which is where the
authentication header injection lives — one place, typed, no duplicated header logic
in individual services.

The migration strategy: one service at a time, starting with the services that make
the most requests. I converted the `ShiftsService` first, ran it through the integration
tests, and shipped. Then `FacilitiesService`, `UsersService`, the rest in order. No
big-bang switch — the old and new modules can coexist during migration. The whole
conversion took four evenings of focused work spread across a week. Not glamorous,
but the codebase is measurably cleaner on the other side.
