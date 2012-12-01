---
layout: post
title: "November notes"
date: 2012-11-30
author: marcetux
tags: [meta, retrospective]
---
The theme of the month, looking back, was **resilience and reading the machine.**
Message queues so work survives a crash. Redis so reads survive load. Execution
plans and better logging so I can actually see what the system is doing instead of
guessing.

A through-line I didn't plan: most of this month's wins were about *not losing
information* — a message the broker holds onto, a log line specific enough to
reproduce a bug, a Pi that restarts its own logger after a power cut. Reliability is
mostly refusing to drop things on the floor.

The CSV formatter and the pre-commit hook went into `examples/`. December I want to
get serious about testing — NUnit and Moq have been on the someday list too long.
Onward.
