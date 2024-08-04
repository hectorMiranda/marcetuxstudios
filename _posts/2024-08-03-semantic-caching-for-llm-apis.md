---
layout: post
title: "Semantic caching for LLM APIs"
date: 2024-08-03
author: marcetux
tags: [llm, caching, ai, performance, architecture]
---
Token costs on LLM APIs are real and they compound at scale. A consulting client was
spending more on inference than their hosting bill and when I looked at their access
logs, fifteen percent of requests were semantically identical — same question,
slightly different phrasing. "What are your return hours?" and "When do returns
close?" both want the same answer from the same data. Standard caching doesn't catch
this because the cache key is the literal request string. Semantic caching does.

The implementation: before sending a request to the LLM, embed the query and check
for similar embeddings in a fast store (Redis with a vector index, or a light in-
memory index for low volume). If there's a hit above a similarity threshold, return
the cached response without hitting the API. On a miss, call the API, store the
embedding and response. The threshold tuning is where the work is — too high and you
miss obvious duplicates, too low and you return responses for queries that were
similar but not the same. I set it empirically: start at 0.92 cosine similarity,
sample false-positive and false-negative rates for a week, adjust.

For the client, fifteen percent cache hit rate at a 93-percentile threshold dropped
their monthly inference bill by eleven percent in the first month. Not huge, but free
money and better latency on cache hits. The storage cost was negligible. The trap
to avoid: don't cache responses to queries where freshness matters — check inventory,
get current prices. Semantic caching is for stable knowledge, not live data. Know
which queries are which before you set the threshold.
