---
layout: post
title: "Multi-hop RAG when one retrieval step is not enough"
date: 2024-06-03
author: marcetux
tags: [rag, llm, ai, retrieval, architecture]
---
Single-hop RAG is retrieving once based on the original query, then generating.
That works for a large fraction of questions. It breaks down when the answer requires
combining information from multiple sources that aren't co-located in the index —
when the right chunks can only be found given information from earlier retrieval
steps. The canonical example from consulting work: "What's the refund policy for
purchases made during a promotional period?" The promotional-period details are in
one document; the refund policy is in another; neither retrieves the other when
queried directly.

Multi-hop retrieval addresses this by treating retrieval as a chain. The first
retrieval step gets context; a planning step (usually a lightweight model call)
identifies what's still missing; a second retrieval step targets the gap. You can
go three or four hops, though cost and latency stack up fast. In practice I've found
two hops covers nearly every real case and the added complexity of three-plus rarely
justifies itself on actual workloads versus well-constructed benchmark questions.

The implementation detail that matters: the second query needs to be generated, not
templated. It has to incorporate what the first retrieval returned, which means the
planner model needs to produce a *new query* rather than just re-running the original
one. I log both the original question and each derived query so I can see where the
chain is working and where it's producing redundant retrievals. Multi-hop is the
right answer for a specific class of queries; the trick is identifying that class
without routing everything through it.
