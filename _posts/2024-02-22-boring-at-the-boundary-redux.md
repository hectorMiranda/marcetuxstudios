---
layout: post
title: "Boring at the boundary, revisited"
date: 2024-02-22
author: marcetux
tags: [architecture, engineering, design, consulting]
---
The "boring at the boundary" rule keeps coming up in consulting work and I want to
write it down more precisely than I usually say it. The boundary is any place where
your system hands off to another system or person: an API surface, a message schema,
a file format, a CLI argument. At that boundary, the cost of a clever choice is paid
by everyone who ever touches it — not just you, not just now.

A recent client had a webhook payload with a field called `status` that could mean
three different things depending on another field called `mode`. The person who
designed it was being efficient; it's fewer fields. But reading the docs, debugging
a misfired webhook at midnight, or onboarding an integration partner — all of those
carry the cognitive load of a two-variable lookup table that turns out to have
inconsistencies. A dedicated field per semantic meaning would have been three more
words in the JSON and a complete elimination of the ambiguity class.

I keep landing on the same heuristic: at the boundary, choose the option a tired
colleague can't misuse. Not because your colleagues are incompetent — they're not —
but because tired and under pressure is when the clever double-use field reveals its
true cost. Twelve years in I've designed both kinds of boundaries. The boring ones
have not caused me a single incident review.
