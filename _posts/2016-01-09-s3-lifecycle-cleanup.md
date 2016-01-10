---
layout: post
title: "S3 lifecycle rules for media cleanup"
date: 2016-01-09
author: marcetux
tags: [aws, s3, media, cost, devops]
---
Video platforms accumulate storage debt fast. JibJab's pipeline keeps the source upload,
the mezzanine, the delivery renditions, and sometimes a manifest — several copies of the
same event, some of which only exist for intermediate processing. For a long time we
deleted old uploads reactively, on a case-by-case basis. S3 lifecycle rules let me
stop thinking about it.

A lifecycle rule is a JSON policy attached to a bucket or prefix: after N days, move
objects to Glacier; after M days, expire them entirely. I split the prefixes by purpose
— `uploads/raw/` transitions to Glacier after 30 days and expires after 90; `transcode/
mezzanine/` expires after 7 because we don't need it once the renditions are done;
`delivery/` never expires automatically because the CDN pulls from there. One policy
document, no Lambda, no cron job.

The cost drop showed up the next billing cycle. The trickier win was the mezzanine
expiry: those files were staying around indefinitely because no code explicitly deleted
them, and nobody was looking. Lifecycle rules are the place to record "this intermediate
artifact exists for N days and then it doesn't" rather than trusting application code to
remember to clean up after itself. Every bucket with a defined retention story should
have one.
