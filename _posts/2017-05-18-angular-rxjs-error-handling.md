---
layout: post
title: "RxJS error handling in Angular HTTP calls"
date: 2017-05-18
author: marcetux
tags: [angular, rxjs, typescript, frontend]
---
The seller dashboard has a pattern problem: HTTP errors are handled differently in every
service. Some use `.catch()`, some just let the observable throw and rely on the
component to handle it, some swallow errors silently. The result is that a user action
that fails an API call produces one of three outcomes depending on which service handles
it, and only one of those outcomes notifies the user.

The fix I landed on is a global HTTP interceptor. Angular's `HttpClient` supports
interceptors — classes that sit in the request/response pipeline and can modify or
react to either. An error interceptor catches any `HttpErrorResponse`, classifies it —
4xx is an application error, 5xx is a server error, network failure is its own
category — and passes it to a notification service that shows the right message and
logs to the console. Individual services never handle errors they can't recover from;
they let them bubble to the interceptor.

The services that can recover — retry on 429 (rate limit), refresh token on 401 —
handle those specific cases locally with RxJS `retryWhen` and `catchError`, and re-
throw anything they can't resolve. The interceptor catches what falls through. Two
layers, clear responsibilities: local recovery logic where it matters, global
notification for everything else. The dashboard went from "sometimes tells you when
something failed" to "always tells you, consistently."
