---
layout: post
title: "LangChain, first look"
date: 2023-02-15
author: marcetux
tags: [llm, langchain, python, ai]
---
Everybody in the AI Twitter bubble is talking about LangChain, so I put an evening into
reading the source and building a small retrieval pipeline with it. The library is
Python, which I use occasionally but not daily, and the abstraction layer it introduces
over the OpenAI API is opinionated in ways that took me a while to make peace with.

The core concept is a chain — a composable sequence of steps where each step
transforms text and passes it forward. The practical pattern: split documents into
chunks, embed them into a vector store, then at query time retrieve relevant chunks and
pass them as context into a completion call. LangChain has primitives for all of this:
`TextSplitter`, `OpenAIEmbeddings`, `Chroma` as a local vector store, and
`RetrievalQA` to wire them together. The demo takes maybe 30 lines. It works.

My hesitation is that the abstraction layer is very thick. There are a lot of classes
between my code and the API call, and the documentation doesn't always tell you what's
actually happening at the HTTP level. When something breaks — and something always
breaks in a library moving this fast — debugging through the chain is a pain. I'll keep
using it for prototyping because the velocity is real. For anything I ship, I'm going
to understand what's under the chain.
