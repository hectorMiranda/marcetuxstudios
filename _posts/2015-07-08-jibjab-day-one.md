---
layout: post
title: "JibJab, day one impressions"
date: 2015-07-08
author: marcetux
tags: [jibjab, career, aws, video, ember]
---
Day one at JibJab. The stack is immediately different from anything I've run in
production before: Ember.js on the front end, Python for the video rendering pipeline,
Ruby for the API layer, AWS running more infrastructure than I've seen in one account.
The S3 usage alone is in a different category — billions of objects across multiple
buckets, a lifecycle policy per bucket, CloudFront in front of all of it.

The part I'm most interested in is the video rendering pipeline. JibJab's product is
personalized video — you upload a face, it gets composited into a holiday card or music
video — and the pipeline is FFMPEG at the core with a queue-driven orchestration layer.
A render job hits SQS, a worker pulls it, FFMPEG does the heavy lifting, the output goes
to S3, a notification goes back through SQS to the API. The pattern is familiar from
Spark's match pipeline; the payload is a lot more interesting.

The team is smaller than Spark — ten engineers, maybe — which means context switching
between infrastructure work and feature work without the luxury of specialization. I
expect to touch the Ember codebase before I fully understand the video pipeline and
vice versa. That breadth is what I wanted. Picking up Ember while doing FFMPEG while
learning a new AWS account is a busy first week and exactly right.
