---
layout: post
title: "June notes"
date: 2013-06-28
author: marcetux
tags: [meta, retrospective]
---
June split into three threads and they're all running in parallel in my head. The
Snowden revelations changed how I think about what "secure by default" means — I'm
putting everything behind HTTPS, not just the auth pages, and I actually understand
TLS termination at the CDN layer now instead of just knowing the phrase. The
privilege of working next to a CDN is you can't pretend you don't understand the
network.

The tooling thread: Web API 2's queryable endpoints collapsed eight report-filter
variations into one; the HTTP spec reading fixed a `DELETE` that was returning the
wrong status code and clarified `PUT` versus `POST` idempotency for three endpoints;
Angular watch performance went from 1,800 watchers to 400 with one-time bindings and
`track by`. Batarang is a permanent part of the debug toolkit.

And the v1.1 boards are fully working — clean assembly, no bodge wires, Pi talking to
the logger over the new UART header as intended. July: I want to revisit the OAuth2
token refresh flow (the access token expires and we're currently just kicking users
out), dig deeper into the OWIN auth pipeline, and figure out what a sensible TLS policy
document looks like for the team.
