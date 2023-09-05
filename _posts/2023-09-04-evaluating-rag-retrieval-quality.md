---
layout: post
title: "Evaluating RAG retrieval quality"
date: 2023-09-04
author: marcetux
tags: [rag, llm, evaluation, ai]
---
The hardest part of building a RAG pipeline isn't the happy path — it's knowing when
retrieval is failing silently. The model generates a confident-sounding answer whether
the retrieved chunks were relevant or not, so "the output looks reasonable" is not an
evaluation strategy. You need to measure retrieval quality independently of generation
quality, and those require different metrics.

For retrieval, the standard metrics are precision and recall at k: of the top-k chunks
returned, what fraction were relevant (precision), and of all relevant chunks in the
store, what fraction were in the top-k (recall). Computing these requires labeled data
— a set of query/relevant-document pairs you can build by hand or, for early prototypes,
by having the full-context model tell you which chunks should have been retrieved. That
second approach is bootstrap labeling and it's imperfect but it gets you running.

For generation, given a question and the retrieved context, you can ask a model to
score whether the generated answer is faithful to the context (not adding facts the
context doesn't contain) and relevant to the question. This is often called "answer
faithfulness" and "answer relevance" in the RAG evaluation literature. The tool I've
started using is RAGAS — a Python library that automates this evaluation pattern using
model calls. It's not free (it makes OpenAI calls to score), but for a weekly eval
run on a test set it's affordable and more consistent than manual review.
