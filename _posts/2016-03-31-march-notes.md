---
layout: post
title: "March notes"
date: 2016-03-31
author: marcetux
tags: [meta, retrospective]
---
March was productive. The KiCad board came back from the fab in perfect shape — ten
working boards and a workflow I now trust. ECR is running in production; the AMI-as-
artifact antipattern is gone. Let's Encrypt went on a real internal service without
incident and the renewal automation has been silent in the right way. SQS workers are
now idempotent, which means the rare duplicate delivery is handled gracefully instead
of doubling our transcoding spend.

The Angular 2 beta spelunking was useful even if I'm not shipping with it yet — the
component model clicks in a way that Angular 1's controller/scope abstraction never
fully did. The signed URL work on CloudFront closed a content-protection gap that I'd
been uncomfortable with for longer than I should have been. Locking down the origin
bucket so nothing bypasses CloudFront was the other half of that; it's the correct
posture and it was a one-line bucket policy change.

April goal: the kitchen sensor board needs firmware. I want one environmental sensor —
temperature, humidity, something simple — posting to Mosquitto on the Pi 3 and showing
up on a dashboard I don't have to SSH into to read. Two weeks, achievable.
