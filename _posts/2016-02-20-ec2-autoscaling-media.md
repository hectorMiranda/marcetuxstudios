---
layout: post
title: "Auto Scaling Groups for stateless media workers"
date: 2016-02-20
author: marcetux
tags: [aws, autoscaling, ec2, media, devops]
---
Transcoding is CPU-bound and bursty — a popular clip goes viral and fifty simultaneous
upload events hit the queue, then things go quiet for hours. Keeping enough instances
running to absorb the peak wastes money during the quiet; not keeping enough means
users wait. EC2 Auto Scaling Groups with SQS-depth-based scaling is the answer to
that, and we got ours tuned reasonably this month.

The scaling policy watches the `ApproximateNumberOfMessagesVisible` CloudWatch metric on
the transcoding queue. When the depth exceeds a threshold — say, 20 messages per
worker — a scale-out policy fires and adds instances. When the queue drains, a scale-in
policy fires after a cooldown. The scale-out needs to be aggressive because transcoding
is slow — a 30-second video might take 20 seconds to process — and you don't want to
still be scaling while the queue grows. Scale in conservatively, scale out fast.

The stateless worker design is what makes this work. A worker picks a message, downloads
the source from S3, transcodes, uploads the result to S3, deletes the message. No local
state survives an instance termination; an interrupted job reappears in the SQS queue
after the visibility timeout. Scale-in just stops sending new jobs to the instance and
waits for the current one to finish. The pattern is old; it just matters more when you
need it to stretch across a 10× traffic spike at two in the morning.
