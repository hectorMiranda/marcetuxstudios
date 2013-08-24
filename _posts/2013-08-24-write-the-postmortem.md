---
layout: post
title: "Writing the postmortem no one wants to write"
date: 2013-08-24
author: marcetux
tags: [process, devops, engineering, team]
---
We had an outage last week — a caching configuration change propagated to the edge
faster than expected and served stale error pages to customers for about forty minutes.
Nobody's fired, the issue was fixed, and the natural impulse was to move on. Instead
I wrote the postmortem that nobody had asked for.

A postmortem isn't a blame document; it's a causal analysis. The five-whys framing
works: why did customers see stale errors? Because the error pages were cached at the
edge. Why were they cached? Because the response headers set a positive `s-maxage` on
error responses. Why did those headers exist? Because the template was copied from a
success-response config and nobody noticed the cache policy inherited. Why didn't anyone
notice? Because the deploy checklist doesn't cover edge caching behavior for error
paths. Why isn't that on the checklist? Because we'd never hit it before.

The checklist now covers error-path cache headers. That's the outcome. The checklist
item wasn't the fix — the root cause was "we didn't know to check this" — so the fix
is encoding the knowledge where it will be consulted. A postmortem no one reads helps
no one; a checklist item that runs before the next relevant deploy absorbs the lesson
where it can prevent a repeat. Writing it took ninety minutes and was absolutely the
right use of time.
