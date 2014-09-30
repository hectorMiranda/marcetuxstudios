---
layout: post
title: "September notes 2014"
date: 2014-09-29
author: marcetux
tags: [meta, retrospective]
---
Four months at Spark and the learning curve is leveling out in the right way — not
because there's nothing left to learn, but because the unknown unknowns are becoming
known unknowns. I know what I don't know about the Couchbase ops, about the full SQS
worker lifecycle, about the spots in the data model that have never been stress-tested.
That's progress.

The PostgreSQL partial index from this month was my first independently spotted
performance issue at the new job — I noticed the slow query in the logs, traced it to
the full scan, built the fix, and shipped it without the index becoming anyone else's
problem. That felt good in a way I want to remember. Good work is finding problems
before they find users.

The ESP8266 on the door is still running. I added a second one to the medicine cabinet
door because I could; it takes about an hour to wire and flash now that I have the
process memorized. If I wire five more sensors I'll have a home network, which means
I need somewhere to collect the readings. Thinking about setting up a small Rails API
on a Raspberry Pi — keep it local, keep it simple.
