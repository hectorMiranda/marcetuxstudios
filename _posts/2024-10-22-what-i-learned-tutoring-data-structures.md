---
layout: post
title: "What tutoring data structures taught me about my own gaps"
date: 2024-10-22
author: marcetux
tags: [cs, teaching, algorithms, data-structures, career]
---
I've been working through data structures with two students this month — one preparing
for interviews, one in a data structures course — and I keep discovering edges of my
own understanding that feel like they should be sharper after twelve years. Not the
implementations; those I can code from memory. The analysis: why does a skip list
give you O(log n) expected search time? I could explain it with the right drawing,
but "expected" requires talking about the probability of node promotion, and that
explanation is surprisingly subtle to make rigorous without hand-waving.

What's clarifying: the difference between "I can use this correctly" and "I can
explain it from first principles" is real and large, and professional software
engineering mostly rewards the first. You use a hash map correctly every day without
needing to explain the birthday paradox. Teaching requires the second, and rebuilding
those explanations is both uncomfortable and useful.

The practical output: I now keep a personal document of the explanations that stumped
me as a teacher. Not the student's question, but the moment where my answer felt
incomplete. That list has twelve entries so far this year and all twelve are things
I'd say I "know." The list is a map of the gap between using something and
understanding it. I'm working through it one entry at a time. It's slower than
reading a data structures textbook for the third time; it's also more honest about
where the understanding actually is.
