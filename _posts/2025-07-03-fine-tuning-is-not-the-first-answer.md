---
layout: post
title: "Fine-tuning is not the first answer"
date: 2025-07-03
author: marcetux
tags: [llm, fine-tuning, ai, architecture, consulting]
---
Clients come in asking about fine-tuning their model more often than any other LLM
question right now. They've heard it makes the model "theirs," that it learns their
domain, that it fixes the behavior they don't like. Most of them should be doing prompt
engineering and retrieval first, and I spend a good portion of early engagement explaining
why fine-tuning is a tool for a specific problem, not a general upgrade.

The specific problems fine-tuning solves: consistent output format without elaborate
formatting instructions in the prompt, style that exactly matches a corpus (tone, voice,
terminology), and behavior patterns that are genuinely hard to elicit with prompting.
The problems it doesn't solve: factual knowledge about your domain (use retrieval),
up-to-date information (retrieval again), following multi-step instructions reliably
(that's prompt structure). Fine-tuning a model on your company documents to improve
factual accuracy is a common mistake that produces a confident model that's wrong in
different ways than before.

The order of operations: exhaustive prompt engineering first, then retrieval if
knowledge is the gap, then fine-tuning if the remaining problem is style or format.
Most teams fix their problem in step one. The teams that reach step three have a well-
diagnosed specific need, not a vague sense that the model should be smarter about their
domain.
