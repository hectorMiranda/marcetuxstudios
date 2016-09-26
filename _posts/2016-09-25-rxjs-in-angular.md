---
layout: post
title: "RxJS in Angular 2 for async data that changes"
date: 2016-09-25
author: marcetux
tags: [angular, rxjs, javascript, frontend, reactive]
---
Angular 2 ships RxJS as a first-class dependency and the `async` pipe in templates
subscribes to an Observable and unwraps the value for you. I'd been using it mostly as
"like a Promise but you can cancel it," which undersells the model. This month I built
a video clip progress view that polls for job status and updates in real time, and RxJS
is what made the implementation not horrible.

The pattern: `Http.get(url)` returns an Observable. Chaining `.repeat()` on a timer
Observable, `switchMap` to the HTTP call, and `distinctUntilChanged` on the status field
gives me "poll every 5 seconds, emit only when the status changes, stop when the
component destroys." In Promises, that's four variables, a `setInterval`, a `clearInterval`
in `ngOnDestroy`, and a bug where the interval fires after navigation away from the
component. In RxJS, it's a pipeline that composes and automatically tears down.

The mental shift is from "do this sequence of things" to "describe the relationship
between values over time." I don't think about starting and stopping the poll; I think
about "a stream of status values, deduplicated, while this component is alive." The
Angular `async` pipe subscribes and unsubscribes automatically. There's a learning
curve — RxJS has a lot of operators and the wrong one is easy to pick — but the
composable-teardown pattern alone is worth it.
