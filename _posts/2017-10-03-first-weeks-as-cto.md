---
layout: post
title: "First weeks as CTO — what I got wrong about the role"
date: 2017-10-03
author: marcetux
tags: [leadership, startup, career, meta]
---
Three weeks into the role and the first thing I got wrong was how much of it is
communication rather than code. I expected the CTO job to be the same as senior
engineering with more responsibility and a different title. It's actually a different
kind of responsibility — the accountability for technical decisions extends past the
technical org into conversations with the CEO, the investors, and the healthcare
facilities who are the customers. When something breaks, I explain it to people who
don't know what a database is, which is a skill I'd never practiced.

The pattern I'm finding useful: translate everything into outcome and timeline, never
mechanism. Not "the SQS queue was backed up because the consumer pool was undersized
for the spike load" — that's mechanism. "Nurses weren't receiving shift notifications
for about forty minutes on Tuesday morning; the fix is in and the alert is set up so we
catch it before users do next time" — that's outcome and timeline. Same facts, different
frame. The CEO needed the second version. The engineering post-mortem needed the first.

The code I'm writing this month is smaller than I expected — more review, more
architecture decisions, more "should we build this or buy that" — but the code I do
write sets the standard for the first engineer I hired, who started last week. That's
a different kind of weight on a pull request. Every choice I make in the codebase is
tacitly an answer to "how do we do things here."
