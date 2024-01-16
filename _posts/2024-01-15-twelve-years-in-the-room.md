---
layout: post
title: "Twelve years in the room"
date: 2024-01-15
author: marcetux
tags: [career, mentorship, retrospective, engineering]
---
I started tutoring a few CS students last week — undergrads, a bootcamp grad, a
career-changer — and the first session clarified something I hadn't examined in a
while. They ask questions I stopped asking a decade ago, not because the questions
are naive but because I automated past them. Explaining *why* a hash map has O(1)
lookup, from first principles, to someone who genuinely doesn't know yet: I'd
forgotten that's an interesting thing to explain.

Twelve years in the room means the embarrassing bugs have already happened to me.
I've shipped the N+1 query, the forgotten cache invalidation, the CSRF gap that
wasn't a real CSRF gap but cost two days of incident review anyway. Those mistakes
have a weight that a textbook answer doesn't. What I'm trying to pass on isn't the
correct answer — it's the failure mode and why it bites you, and the design move
that makes it less likely. You can read about idempotency; it hits differently when
you've debugged the double-charge.

The unexpected benefit is that teaching clarifies what I actually understand versus
what I've pattern-matched. I thought I understood consistent hashing deeply; I
discovered I understood it well enough to use it and not quite well enough to
explain the ring without drawing a picture first. The gap is useful to find. Twelve
years gives you a lot to teach; it also shows you exactly where the gaps still are.
