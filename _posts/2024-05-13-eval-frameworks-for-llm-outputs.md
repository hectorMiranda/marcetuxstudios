---
layout: post
title: "Eval frameworks for LLM outputs"
date: 2024-05-13
author: marcetux
tags: [llm, eval, ai, testing, quality]
---
The question that keeps coming up in consulting work: how do you know your LLM
pipeline is getting better? "I tried it and it felt better" is not a regression
test. I've been building lightweight eval frameworks for each project and the
structure is always the same, which suggests it's converging on a pattern worth
naming.

The three components: a golden dataset, a judge, and a scorer. The golden dataset
is twenty to fifty (input, expected-output) pairs, built from real production
examples. The judge is a function that takes actual output and expected output and
returns a score — this can be exact match for structured extraction, an LLM-as-judge
for open-ended text, or a combination. The scorer aggregates across the dataset and
writes the result to a log with the commit hash and prompt version. On every prompt
change, run the eval; the number either goes up or you understand why it went down.

The trap I see most often: building the golden dataset from synthetic examples
instead of real ones. Synthetic examples reflect what the developer thought the
hard cases were. Real production examples surface what they weren't. I always seed
the golden set from logs — the inputs where users had to retry, the ones where they
complained, the ones where the output was technically correct but practically useless.
The eval is only as honest as the data behind it.
