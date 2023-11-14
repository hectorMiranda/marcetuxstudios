---
layout: post
title: "Observability for LLM pipelines"
date: 2023-11-13
author: marcetux
tags: [llm, observability, ai, architecture]
---
The LLM pipeline I'm maintaining for a consulting engagement started throwing errors
last week in a way I couldn't diagnose quickly, and the experience clarified what
observability means for a system with a language model in the critical path.
Traditional observability — latency, error rate, throughput — is necessary but not
sufficient. You also need to know what the model said.

The minimum viable observability for an LLM pipeline: log the prompt (or a hash of it
if it contains PII), log the completion, log the retrieved chunks and their similarity
scores, log the latency at each stage separately (retrieval vs generation vs
post-processing), and log the token counts. Token counts matter for cost attribution;
latency by stage matters for debugging; the prompt-completion pair matters for quality
review.

The thing I added after last week: log when retrieval returns no chunks above the
similarity threshold, and treat it as a distinct event, not a successful empty result.
"Retrieved nothing" and "retrieved the wrong thing" look identical in the response if
you don't track retrieval quality. The model fills both cases with training-weight
answers, confidently, and the user's query is gone from the trace unless you kept it.
A retrieval zero-hit rate above 5% is a signal worth a dashboard panel. I didn't have
that panel; I do now.
