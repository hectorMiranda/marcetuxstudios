---
layout: post
title: "Writing a patch runbook when every minute costs"
date: 2014-04-07
author: marcetux
tags: [devops, security, operations, incident, process]
---
Heartbleed response taught me that a good runbook is worth writing before you need it,
not during. The first few hours of the response were faster than they would have been
a year ago because we had the skeleton of an incident process — assigned roles, a
shared log, a communication channel. The parts that slowed us down were the steps
that required judgment under pressure: is this server affected? what version of OpenSSL
is the runtime linking against at execution time, not at install time?

A runbook written in the moment will miss the judgment steps, because you're in the
middle of them. A runbook written afterward will miss them too, because the painful
ones are easy to forget once the adrenaline fades. The runbooks that work are written
after an incident, reviewed when calm, and tested in a drill before the next one. The
test matters — a runbook that no one has followed is a hypothesis, not a procedure.

The specific step I'll add to our Heartbleed runbook now: verify the runtime OpenSSL
linkage, not the package version. A service can run an old OpenSSL build even after a
package upgrade if it was compiled against a static library or launched before the
package was updated. `lsof -p <pid> | grep ssl` or `strings /proc/<pid>/exe | grep
OpenSSL` will tell you what's actually running. That's the step I didn't know to check
on day one.
