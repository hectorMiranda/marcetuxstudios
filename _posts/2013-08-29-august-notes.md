---
layout: post
title: "August notes"
date: 2013-08-29
author: marcetux
tags: [meta, retrospective]
---
Lighter month. The Bootstrap 3 release was the big external event — mobile-first by
default is the right direction, and I'm on it for anything greenfield. Angular unit
tests are finally catching up with six months of untested service code; the Jasmine
plus ngMock combination is the environment I should have set up in February.

CORS turned out to be a half-day fix once I understood why the browser was silently
dropping preflight requests. That kind of "it just doesn't work and there's no error"
failure mode is the worst to debug the first time and trivially obvious the second.
The postmortem from the caching outage is the most important thing I did this month —
the checklist item will outlive the memory of the outage.

September: I want to revisit the Grunt build now that TypeScript compile time is
growing, consider whether Gulp would be faster for the TypeScript/SASS steps specifically,
and think seriously about a proper integration test suite for the Web API layer. The
unit tests on services are good; the contract between Web API controllers and the
underlying services is still tested only by hand.
