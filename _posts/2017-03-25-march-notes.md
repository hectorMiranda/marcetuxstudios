---
layout: post
title: "March notes"
date: 2017-03-25
author: marcetux
tags: [meta, retrospective]
---
March felt like the month where the infrastructure work started compounding. Kubernetes
in production — even a small cluster for the Node services — removed the manual deploy
ceremony that was costing time every sprint. The RBAC work landed and immediately paid
off when a support request came in from a seller employee who needed read-only access:
I added a `support-viewer` role, assigned the right permissions, done. Previously that
would have meant a code change.

The Pi cluster at home is a hobby and a learning environment, and I'm comfortable with
that distinction. Understanding Kubernetes by breaking it on hardware I own, without a
cloud bill to worry about, is a different kind of learning than the cloud setup at work.
Both are useful. The HA setup is also stable — 30-day uptime on the Pi running Home
Assistant, five sensors reporting, a handful of automations running.

The inventory idempotency work was the invisible win of the month: the audit log clean,
no duplicate feeds, the channel APIs not throttling us for redundant updates. Code that
does nothing wrong when called twice is boring to write and satisfying to not have to
debug at 11pm. More of that.
