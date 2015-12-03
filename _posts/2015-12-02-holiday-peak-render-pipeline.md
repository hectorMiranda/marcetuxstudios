---
layout: post
title: "Running the render pipeline through holiday peak"
date: 2015-12-02
author: marcetux
tags: [aws, sqs, jibjab, video, scaling]
---
The week after Thanksgiving is JibJab's Black Friday — the traffic spike is real and
sustained, and everything we built in the fall earned its keep. Queue depth on the
render queue hit six times the summer baseline; Auto Scaling brought up eight workers
within four minutes; the dead-letter queue received eleven messages over the whole week,
all traced to a single new template with a malformed filter graph that went undetected
in QA. Everything else processed cleanly.

The observability work paid off in a way I'd hoped but didn't expect to measure so
concretely. The malformed-template failures were visible in the DLQ within 20 minutes
of the first user submission. The structured CloudWatch logs showed the exact step and
the exact FFMPEG error. The template was pulled, fixed, and re-deployed while users
who had submitted with it got their jobs re-queued from the DLQ. Total time from first
failure to resolution: 47 minutes. Without the logging and the DLQ, that's a customer
support ticket pile and a multi-hour guessing session.

The one thing I'd change: the auto-scaling policy scale-in wait of 15 minutes was
correct but the workers during scale-in were idle and we were paying EC2 for them.
Spot instances with a bid price slightly above the Spot price would cut the idle-worker
cost significantly. That's the December project if peak quiets down before Christmas.
