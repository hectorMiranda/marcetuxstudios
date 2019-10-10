---
layout: post
title: "Observability maturity, a mid-year check"
date: 2019-10-09
author: marcetux
tags: [observability, splunk, dynatrace, architecture, banking]
---
Nine months into the observability overhaul at the bank. The three pillars — structured logs in Splunk, distributed tracing in Application Insights, APM in Dynatrace — are operational. The question I've been asking is: can we answer "what's wrong" in under five minutes for any production incident? The honest answer is: for most incidents, yes. For some, not yet.

The "not yet" category is incidents that cross a seam we haven't fully instrumented. The payment service is fully traced. The third-party payment network is a black box. When a payment fails with a network error that originates in the third-party system, our trace ends at the outbound HTTP call and we have a status code. The third-party's incident report usually arrives hours later. We can see what we sent and when; we can't see what happened inside their system. That's a permanent limitation, but we can add more context to our side of the seam — recording the full outbound request in the audit log and correlating it with the status we received.

The metric I use to measure observability maturity: how many on-call runbook items still say "check the database by hand"? In January that was seven. It's two now. Both are genuinely hard to instrument automatically and both have tickets. The goal is not perfect observability — it's reducing the number of things that require a human with local knowledge to diagnose. Every runbook item that gets replaced by a Splunk query or a Dynatrace alert is an incident that the next on-call engineer can handle without calling the person who originally built the system.
