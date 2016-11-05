---
layout: post
title: "Writing handoff documentation for things only you know"
date: 2016-11-04
author: marcetux
tags: [documentation, process, career, retrospective]
---
I'm spending Fridays in November writing documentation. The transcoding pipeline is the
obvious target — I built most of it and the person who inherits it will need to
understand the idempotency contract, the DLQ monitoring expectations, the TTL math
for the lifecycle rules, and a dozen other decisions that made sense at the time and
will look arbitrary without the context. That context lives in my head right now. It
shouldn't.

The format I've landed on is ADR-adjacent but less formal: a short document per system
component that covers "what it does," "why it's designed this way rather than the
obvious alternative," and "what will go wrong and how to notice it." The first section
is for the new person; the second is for the curious engineer who wants to refactor it;
the third is for the on-call engineer at 2am. Different readers, same document.

The thing I notice writing these: I have strong opinions about some decisions and no
memory of why I made others. The decisions I can't explain are usually the ones I made
quickly under deadline or the ones that seemed obvious at the time. "Obviously you'd
do it this way" ages poorly. The decisions I remember clearly are the ones where I tried
the obvious thing first and it broke. Write down what broke. The new person will try
the same obvious thing.
