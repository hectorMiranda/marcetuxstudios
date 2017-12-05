---
layout: post
title: "From pilot to contract — what changes when users are paying"
date: 2017-12-04
author: marcetux
tags: [startup, reliability, product, leadership]
---
The first three pilot facilities signed contracts this week, which is the milestone that
turns "we're building this" into "they're depending on this." The change in my posture
is not a policy change — the app was already production-hardened from November — it's a
psychological one. During the pilot, a one-hour outage is an embarrassing incident.
Under contract, it's the first paragraph of an SLA conversation. Same event, different
relationship.

The first thing I did the day the contracts were signed: updated the alerting thresholds
and the on-call rotation, which is currently just me. The p99 latency alert went from
a Slack message to a PagerDuty alert with a five-minute acknowledgment window. I
reviewed every alert that had fired in the last 30 days and classified each one as
"real issue" or "miscalibrated threshold." Miscalibrated alerts are the ones that teach
you to ignore alerts, which is the worst outcome — you want every alert to mean
something is actually wrong.

The other change: I wrote an incident response runbook specifically for the scenarios
most likely to affect nurses during a shift-posting window. 6pm to 8pm is peak usage;
a database connection pool exhaustion during that window hits real people in real time.
The runbook tells the on-call person exactly what to check, in what order, and what
to do if the check fails. The best runbook is the one that never gets used. The second-
best is the one that gets used and works.
