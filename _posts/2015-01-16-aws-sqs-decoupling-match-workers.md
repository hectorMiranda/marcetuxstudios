---
layout: post
title: "SQS and decoupling the match workers"
date: 2015-01-16
author: marcetux
tags: [aws, sqs, architecture, backend, ruby]
---
The match-suggestion pipeline at Spark used to be a tight loop: web request triggers
computation, computation writes the result, response waits for all of it. That worked
at low traffic and fell apart the moment the profile count grew. The fix was obvious in
retrospect — SQS as a work queue, web tier enqueues a match job, a pool of worker
processes drains the queue independently.

SQS's model is simple enough that it's almost boring: you put a message on the queue,
a worker pulls it, processes it, deletes it on success. Fail to delete within the
visibility timeout and SQS re-enqueues automatically — you get retry for free, without
building it. The catch is the visibility timeout needs to be longer than your worst-case
processing time or you'll get double-processing, which for a match job means two sets of
suggestions overwriting each other. Tuning that timeout to 2× the P99 processing time
solved the duplicates.

The other thing SQS made trivially easy: scaling workers independently of the web tier.
During evening peak we run more workers; overnight we drop to one. The web tier doesn't
know or care. I keep gravitating toward SQS for any task where "fire and eventually
process" is acceptable because the blast radius of a worker failure is bounded: the
message comes back, a different worker handles it, no data lost.
