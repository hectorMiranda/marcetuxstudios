---
layout: post
title: "Blameless incident reviews in a blame-adjacent culture"
date: 2019-11-26
author: marcetux
tags: [process, teams, reliability, banking, culture]
---
We had a meaningful incident in October — the load-related latency spike I found in load testing, except it happened in a lower-volume staging environment because a client misconfigured a retry loop. The staging incident didn't affect customers but it revealed the problem and triggered the review that found the connection pool sizing. The incident review after that was the first one I ran in this organization, and the culture going in was not naturally blameless.

The frame I use: the system failed, and the system includes the humans and the processes, not just the software. If a person made a choice that contributed to the incident, the question is not "why did they make that choice" as an accusation but "what about the system made that choice look reasonable at the time." Usually the answer is: missing information, missing guardrails, a runbook that didn't cover the case, or a design that made the problematic choice the easy one. Fix those.

The output of a useful incident review is specific action items with owners and dates, not a list of things that went wrong assigned to the people who did them. We added a connection pool health check to the observability dashboard. We updated the load test to run monthly on a schedule rather than on demand. We updated the API consumer integration guide to document the expected retry behavior with backoff. None of those action items is "developer X should do better." All of them are things that will help the next person who hasn't made that mistake yet.
