---
layout: post
title: "LlamaIndex vs LangChain, a practical comparison"
date: 2023-10-03
author: marcetux
tags: [llm, langchain, llamaindex, rag, ai]
---
I've been running both LangChain and LlamaIndex on the same RAG use case for a few
months now, which gives me a fair comparison rather than the kind of take you get from
someone who spent a weekend with one of them. The libraries have different design
philosophies that make each better suited for different problems.

LlamaIndex is opinionated about indexing and retrieval — that's what it was built to
do. The document loading, chunking, embedding, and vector store abstractions are
first-class. If your application is fundamentally "build a search index over documents
and query it," LlamaIndex's defaults are better calibrated than LangChain's. The
`QueryEngine`, `RetrieverQueryEngine`, and the re-ranking pipeline feel like they were
designed by people who had already built a lot of RAG systems.

LangChain is a broader toolkit — chains, agents, tools, memory — and the retrieval
components exist but sometimes feel like they were added to an agent framework rather
than designed from the ground up. For anything that needs RAG as one step in a larger
agent-driven workflow, LangChain's flexibility is the right trade. For anything that is
primarily RAG with no agent behavior, LlamaIndex's narrower scope produces cleaner code.
My current setup: LlamaIndex for the retrieval layer, LangChain for the orchestration
layer that wraps it. Two libraries is one more than I'd like, but the combination is
better than either alone.
