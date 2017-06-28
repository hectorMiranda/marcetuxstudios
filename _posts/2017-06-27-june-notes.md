---
layout: post
title: "June notes"
date: 2017-06-27
author: marcetux
tags: [meta, retrospective]
---
June was the month Kubernetes became real at SolidCommerce rather than a personal
infrastructure hobby. The feed transformation service running on the cluster in
production, Helm managing its config, health probes doing actual health checks — that's
the stack I wanted to land when I started the home cluster experiment in March. Three
months of personal tinkering, one month of production work. That ratio is usually about
right.

The GraphQL schema redesign is moving in a direction I'm happier with. Designing the
schema around consumer queries rather than database tables sounds obvious written down,
but I had to get the wrong version into a PoC to see clearly why it mattered. The team
is doing real query work against it now, which is the fastest way to find the next
wrong assumption.

Home side: KiCad boards ordered for the garage sensor, breadboard still alive on the
shelf. The door reed switch is wired and working — the interrupt wakes the ESP32,
it publishes the door state, goes back to sleep. Home Assistant shows OPEN or CLOSED
with a timestamp. Next step is a notification if the door stays open more than fifteen
minutes while the house is in away mode. Small automation, real utility.
