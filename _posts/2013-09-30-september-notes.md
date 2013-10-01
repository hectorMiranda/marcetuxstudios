---
layout: post
title: "September notes"
date: 2013-09-30
author: marcetux
tags: [meta, retrospective]
---
September was the testing and build month. Gulp replaced Grunt; the front-end build is
faster and more readable. Integration tests on the OWIN self-host found real bugs in
the routing and model binding layers. Protractor E2E tests confirmed the user flows
work end to end, which is a different confidence level than unit tests can give.

The architectural gains: async/await across the Web API layer means threads aren't
wasted on I/O wait; Redis pub/sub solved the configuration notification problem at the
right level of complexity; CSS specificity is now disciplined instead of accumulated.
`git bisect` found a regression in three minutes that would have taken hours of
manual commit archaeology.

October: the Q4 push starts and I expect the personal-project time to compress.
I want to write something about the general arc of what I've been building this year —
the Angular/Web API/Redis/OWIN stack feels like it has a coherent shape now. Also:
Bitcoin is doing interesting things and I have some opinions about the CDN caching
patterns I keep seeing for high-traffic financial API endpoints.
