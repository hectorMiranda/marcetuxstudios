---
layout: post
title: "Feature flags and shipping unfinished work"
date: 2013-01-28
author: marcetux
tags: [deployment, feature-flags, devops, architecture]
---
Continuous deployment created a tension I didn't expect: if every green build can
ship, what about work that's half-done? You don't want a long-lived branch drifting
out of sync for three weeks. Feature flags are the way out, and they've quietly
changed how I build.

A flag is just a conditional around new behavior, backed by config you can flip
without a deploy. The half-finished report ships to production every day — dark,
behind a flag that's off in production and on for me. The code integrates
continuously; the *feature* releases when it's ready, to whoever I choose first.

The discipline is **cleaning them up.** A flag is debt — a fork in your code's
behavior — and a codebase full of stale flags nobody dares delete is its own kind of
mess. My rule: a flag gets a ticket to remove it the day it's created. Flags are
scaffolding for shipping safely, not permanent architecture. Put them up, take them
down.
