---
layout: post
title: "Multi-modal agents in practical consulting work"
date: 2025-11-07
author: marcetux
tags: [ai, agents, multi-modal, vision, consulting]
---
Vision capabilities in the frontier models have matured to the point where I'm shipping
them in client work, not just evaluating them. The use cases that hold up in production
aren't the dramatic ones — they're the boring ones. Extracting data from scanned
documents, classifying product images, validating that a generated UI matches a design
reference. The common thread is that the input happens to be an image and a human would
normally do the extraction or classification by looking at it.

The implementation pattern is the same as text: structured output schema, an eval suite
of representative examples, output validation before downstream use. The only additional
concern with images is cost — image tokens are priced differently and a high-volume
image processing workflow can be surprisingly expensive. The right response is usually a
hybrid: run a cheap classifier to triage which images need the full model treatment and
route the rest to a simpler handler. Most images in a document processing workflow are
routine; the expensive model is for the ones that aren't.

The pattern that hasn't held up: using vision for any task where OCR precision matters.
Character-for-character accuracy on handwritten numbers or unusual fonts is worse than
a purpose-built OCR pipeline, even though the general model looks impressive on
representative samples. Evaluate on the hard cases, not the easy ones.
