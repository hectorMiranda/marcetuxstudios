---
layout: post
title: "Splunk dashboards that operations actually uses"
date: 2019-08-09
author: marcetux
tags: [observability, splunk, operations, banking, monitoring]
---
Splunk has been ingesting our structured logs since January. By August we had enough data to build dashboards the operations team would actually look at rather than dashboards that felt complete to the engineers who built them. The distinction turned out to be significant — the first version was full of panels that answered "what is the system doing" and almost no panels that answered "is something wrong right now."

The redesign started with the operations team's runbook. Every item on the runbook that said "check the logs" became a Splunk panel on the ops dashboard: payment processing error rate in the last five minutes, failed authentication attempts by source IP, Service Bus dead-letter queue depth, API gateway latency p95 and p99 by endpoint. Each panel has a threshold line drawn on it — not the absolute threshold, just a visual reference for "this is what normal looks like." Operations doesn't need to know what normal is; they need to see when the line is far from normal.

The second version of the dashboards has an interesting property: the operations team updates them. They add panels when they find themselves asking a question the dashboard doesn't answer. The Splunk queries are saved searches with human-readable names so an ops engineer can read and modify a query without knowing our log schema from memory. The dashboard is infrastructure, not a deliverable. The team that lives in it during incidents should own it, and architects should be willing to teach the query syntax instead of keeping it a specialist skill.
