---
layout: post
title: "Last week at JibJab and what a media platform teaches you"
date: 2016-12-05
author: marcetux
tags: [career, retrospective, media, video]
---
Friday is my last day. The handoff is done — code review, architecture walk-through,
documentation merged, the on-call rotation covered through the holidays. The team
knows where everything is. I feel good about how it ended, which is the only thing you
can control about an ending.

JibJab is a media platform, which means the problems are about bits in motion: bits
encoded into video, bits stored in S3, bits delivered across a CDN, bits flowing
through queues between workers. Spend a year in that domain and you think about
bandwidth, latency, encoding efficiency, and delivery reliability in a concrete way you
don't from reading about them. The FFMPEG command line becomes a tool you know rather
than one you copy from Stack Overflow. The CDN edge becomes a piece of infrastructure
you optimize rather than one you trust blindly.

The thing I'm taking forward is the pipeline pattern: queue-driven, stateless workers,
each step independent, the authoritative state in a database not in the message. It's
applicable to any workflow that can be decomposed into steps, which is most workflows.
The specific tools change. The pattern is portable.
