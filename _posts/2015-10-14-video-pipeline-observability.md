---
layout: post
title: "Observability in the video render pipeline"
date: 2015-10-14
author: marcetux
tags: [aws, logging, monitoring, jibjab, devops]
---
A render job is a multi-step async process: enqueue, pick up, transcode, thumbnail,
upload, notify. Any step can fail silently if the worker catches an exception and
re-enqueues rather than logging clearly. After a morning debugging a holiday-season
failure that turned out to be a silent S3 permission error, we added structured logging
across every stage and the next incident took six minutes instead of two hours.

Every log line now carries the render job ID, the current step name, the outcome (start,
success, failure), and timing. JSON format, shipped to CloudWatch Logs. The job ID is
the correlation key that lets you filter a single job's complete history across multiple
workers. A search for `render_job_id=abc123` in CloudWatch Logs gives you every step,
every timing, every error for that specific job — no grepping across worker log files,
no lost context when the worker that processed a step has been replaced by autoscaling.

The timing data is the part I didn't anticipate being useful and turned out to be the
most useful. Plotting mean and P95 FFMPEG transcode time per template reveals which
templates are pathologically expensive — filter graph complexity doesn't correlate
linearly with render time, and some templates have filter chains that are 5x slower than
others with no obvious reason. You only know this if you measured it. Structured logs
are not just debugging artifacts; they're the measurement system for understanding what
your pipeline actually does at runtime.
