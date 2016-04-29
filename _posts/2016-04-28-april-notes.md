---
layout: post
title: "April notes"
date: 2016-04-28
author: marcetux
tags: [meta, retrospective]
---
The kitchen sensor is live and the data is boring in the best way — a graph of kitchen
temperature over the past three weeks with two obvious dips when the AC kicked on, one
spike when I left the oven door open too long. The firmware works, the MQTT pipeline
works, and I don't have to do anything for it to keep working. That's the target.

Kubernetes got a fair look this month and I came away respecting it without thinking it's
the right tool for us right now. The Lambda-as-glue pattern paid off immediately on the
S3 event pipeline; that's a keeper. PWAs are real technology dressed up in Google
marketing. TypeScript 2.0 is improving the type system in ways I'll actually use.

May: I want to finalize the TypeScript 2.0 migration on at least one production codebase
and see whether the discriminated union pattern survives contact with real data. Also need
to plan what comes after the current sprint of pipeline reliability work — the team has
been asking about content delivery improvements and I should get ahead of that conversation.
