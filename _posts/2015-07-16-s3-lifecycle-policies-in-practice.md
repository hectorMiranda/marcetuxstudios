---
layout: post
title: "S3 lifecycle policies at JibJab scale"
date: 2015-07-16
author: marcetux
tags: [aws, s3, storage, devops]
---
The S3 bill at JibJab is one of those line items that makes sense once you understand
the business: billions of video and image objects, most of which were accessed heavily
the week they were created and are now effectively cold storage. S3 lifecycle policies
are how you stop paying Standard storage prices for objects nobody has touched in six
months.

The lifecycle configuration attaches to a bucket and is a set of rules: after 30 days,
transition to S3 Infrequent Access; after 180 days, transition to Glacier. The pricing
difference between Standard and Glacier is roughly 10x, and the access pattern for
seven-year-old personalized holiday videos is essentially zero. The transition is AWS-
managed — you define the rules once and objects age through automatically.

The nuance is the access pattern assumption. Infrequent Access has a minimum 30-day
storage charge and a per-retrieval cost. Objects that get retrieved frequently after 30
days will cost more in IA than in Standard. The right policy requires knowing your
access distribution, which means querying S3 access logs or CloudFront request logs.
JibJab tracks request rates per content category and the policy tiers are calibrated
against real data — the holiday videos are reliably cold after 60 days, the GIF output
less so. Lifecycle policies are not set-and-forget; they're a bet on access patterns
that you should verify with logs.
