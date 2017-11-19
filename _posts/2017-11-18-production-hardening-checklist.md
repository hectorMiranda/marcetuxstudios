---
layout: post
title: "Production hardening before a pilot goes to contract"
date: 2017-11-18
author: marcetux
tags: [reliability, production, startup, aws]
---
The pilot facilities go to paying contract in December, which makes November the month
where "working" becomes "production-grade." The distinction is real. A demo that works
most of the time is fine during a pilot. A contracted service that works most of the
time generates an SLA conversation nobody wants to have.

The checklist I worked through: structured logging to CloudWatch with correlation IDs
so I can trace a single shift lifecycle across all the service calls that touch it.
Alerting on error rate above a threshold and on API latency above 500ms for the
endpoints nurses hit most often. RDS automated backups enabled and a tested restore
procedure — I actually ran a restore into a staging database and timed it. Database
connection pool limits set explicitly rather than defaulting to whatever the library
chose. The ECS task health checks updated to check the actual database connection, not
just "is the process running."

The thing about production hardening is that it's mostly making visible things that
were already happening. The error rate alert fired on day one — there was a class of
validation error that was happening silently and successfully (returning 422 to the
client) but with a log level of `Error` rather than `Warning`. That's a calibration
bug in the logging, not a real error, and the alert would have obscured real errors.
Fix the classification before you depend on the alerts. Alerts you don't trust don't
get responded to.
