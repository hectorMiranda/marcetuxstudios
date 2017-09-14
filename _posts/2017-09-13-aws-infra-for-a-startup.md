---
layout: post
title: "AWS infrastructure choices for a startup with a small team"
date: 2017-09-13
author: marcetux
tags: [aws, architecture, startup, devops]
---
The Go RN infrastructure is mine to own now, and the first thing I did was inventory
what exists against what I'd design from scratch. The answer is "reasonably close" —
EC2 instances, RDS PostgreSQL, S3, SQS — but with some decisions that made sense at
the time and are worth revisiting now that I'm the person who'll be debugging them at
midnight.

The managed services principle: if AWS will run it for me reliably, I want AWS to run
it. RDS over self-managed PostgreSQL on EC2. SQS over RabbitMQ. ElastiCache over a
Redis instance I install and patch. The cost difference at startup scale is negligible;
the operational difference — automatic failover, managed backups, no OS patches to
apply — is enormous for a two-person engineering team. I'll spend that money and never
regret it.

The other decision I made early: one region, three availability zones, no multi-region
complexity. Multi-region is a real reliability investment for companies that need it and
an operational tax for companies that don't. Healthcare shift-scheduling is not a
zero-downtime-required application in the way that payment processing is. If a deploy
goes wrong and the app is down for twenty minutes while I roll back, the business
absorbs it. Designing for five-nines availability at a startup means designing an
infrastructure that a small team can't actually operate reliably. Start simple, scale
the complexity when the scale justifies it.
