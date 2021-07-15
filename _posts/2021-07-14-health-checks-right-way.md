---
layout: post
title: "Health checks that actually mean something"
date: 2021-07-14
author: marcetux
tags: [dotnet, kubernetes, reliability, observability]
---
We had a liveness probe pass for four minutes while the service was functionally
dead. The probe was hitting `/health` which returned 200 as long as the process
was alive. The process was alive. The database connection pool was exhausted and
every request was timing out. Kubernetes saw green; customers saw errors.

The problem is the wrong health check at the wrong boundary. A **liveness** probe
asks "is this process in a state it can recover from?" If the answer is no, kill
it and let a new one start. A **readiness** probe asks "is this instance ready to
receive traffic?" If the answer is no, pull it from the load balancer. They are
different questions and a single `/health` endpoint answering both with "the process
is up" serves neither well.

The check we needed for the database case is a readiness check that tests an actual
database round-trip — not a ping to the host, but a real `SELECT 1` through the
connection pool. If the pool is exhausted or the database is unreachable, readiness
returns unhealthy and traffic stops routing to this instance while Kubernetes waits
for recovery. Liveness stays shallow — check the process, not the dependencies —
because a crashed dependency shouldn't kill a healthy process; it should route
traffic away from it. ASP.NET's health check middleware splits these correctly once
you configure what each tag means to your orchestrator.
