---
layout: post
title: "Message queues and the art of not losing work"
date: 2012-11-07
author: marcetux
tags: [messaging, rabbitmq, architecture, reliability]
---
A nightly job that recomputes aggregates failed halfway through this week and took
a chunk of work with it. The fix isn't a better job — it's not doing the work
inline. Put the units of work on a queue and let workers pull them.

A message queue buys you three things that matter here: **durability** (the broker
keeps the message until someone acknowledges it), **retry** (crash mid-task and the
message reappears for another worker), and **back-pressure** (a slow consumer just
drains the queue slower; it doesn't fall over). RabbitMQ is where I landed —
AMQP, durable queues, manual acks.

The mental shift is treating work as messages, not method calls. "Recompute
customer 4821" becomes a message. If a worker dies holding it, it's redelivered.
The job stops being one fragile transaction and becomes a stream of small,
retryable ones.
