---
layout: post
title: "Prompt engineering is engineering and should be treated like it"
date: 2024-05-02
author: marcetux
tags: [llm, prompts, ai, process, engineering]
---
The dismissal I hear most often from senior engineers: "prompt engineering is just
trial and error." I understand the source — the work looks like fiddling with words
until something works. But that description fits debugging too, and we don't dismiss
debugging. The discipline in prompt engineering is the same as the discipline in any
other form of specification: be precise about what you want, measure whether you got
it, and iterate on the specification, not on luck.

The first practice that made prompt work feel like engineering: write a test suite
before writing the prompt. Five to ten representative inputs with expected outputs, a
few deliberate edge cases. Run the prompt against all of them after every change.
This converts "does it feel better?" into "did score improve on these cases?" — an
actual feedback signal instead of an impression. The test suite also documents the
behavior you're committing to, which matters when the prompt is shared or updated
by someone else six months later.

The second practice: change one thing at a time. Moving from zero-shot to few-shot,
changing the persona, restructuring the output schema — all productive moves, but not
all at once. When you change three things simultaneously and performance changes, you
don't know which change caused it. The same discipline as a controlled experiment.
Prompts are specifications, tests are specifications, and the rigor that makes
software maintainable applies here too.
