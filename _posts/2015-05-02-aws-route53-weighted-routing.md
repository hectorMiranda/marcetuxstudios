---
layout: post
title: "Route 53 weighted routing for gradual rollouts"
date: 2015-05-02
author: marcetux
tags: [aws, route53, dns, devops, deployment]
---
Rolling out a significant change to the match-recommendation algorithm to 100% of
traffic on day one seemed like the kind of decision I'd regret, and Route 53's weighted
routing policy made doing the cautious thing nearly free. Two record sets pointing at
the same domain — old algorithm on one set of instances, new algorithm on another —
with a weight ratio that starts at 95/5 and shifts over a week.

Route 53 weighted routing works at the DNS level: you create two records with the same
name and type but different resource IDs and weights, and Route 53 distributes queries
proportionally. An A record with weight 95 pointing at the old load balancer and one
with weight 5 pointing at the new one means roughly 5% of members hit the new
algorithm. Watch the match-engagement metrics for 48 hours; if they're flat or better,
shift to 80/20, then 50/50, then 0/100. If they're worse, shift 100/0 and you've
affected 5% of traffic for 48 hours instead of everyone.

DNS TTL is the one thing to get right before you start: if your TTL is 3600 seconds,
route changes take an hour to propagate and the gradual rollout window is coarse. We
dropped the TTL to 60 seconds a few days before the rollout. The elastic-routing
capability was already there in the infrastructure; the habit of using it is what we
added.
