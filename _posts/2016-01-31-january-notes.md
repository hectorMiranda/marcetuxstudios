---
layout: post
title: "January notes"
date: 2016-01-31
author: marcetux
tags: [meta, retrospective]
---
January felt like a clearing-the-backlog month. Let's Encrypt graduated from beta and I
spent a weekend learning what automated cert renewal actually looks like in practice —
it's as smooth as advertised. The SQS dead-letter queues are in production on the
transcoding pipeline now, and the first time a poison message surfaced in the DLQ
instead of looping through workers for an hour, I felt genuinely vindicated. S3
lifecycle rules went on the media buckets; the bill was noticeably lower.

On the code side, TypeScript's strict null checks found a handful of real bugs I'd
missed — the "of course it's always set" assumption is the one that always gets you.
ES2016 is almost nothing and I mean that as a compliment to the new TC39 cadence. And
KiCad got its first real board from me; the migration from Eagle is going slower than
I'd like but the tool is good.

February goal: get the Pi 3 if it launches (the WiFi story alone makes it worth it)
and wire Let's Encrypt onto something that sees real traffic. The transcoding pipeline
has a couple of reliability improvements I want to push through before they get buried
under the next feature cycle.
