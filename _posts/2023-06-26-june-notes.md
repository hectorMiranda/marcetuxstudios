---
layout: post
title: "June notes"
date: 2023-06-26
author: marcetux
tags: [meta, retrospective]
---
June landed two things at the same time: OpenAI's function calling API, which changed
how I think about LLM integration architecture, and a productive stretch of Rust work
on the CasperLabs client. The months where those two threads are both moving feel
good. The months where one stalls while the other moves feel off-balance.

The vector store operational learning from this month is the kind of thing I wish
someone had written down clearly before I had to debug it: the model-ID tagging, the
stale index problem, the REINDEX discipline. Adding it to the blog is partly for my
own memory and partly because the LLM tutorials are all demos, not operations. The ops
layer of any new technology is always where the real knowledge lives, and it always
takes longer to get written down.

The function calling feature is going to change what's buildable with these APIs.
Not because the capability is entirely new — you could parse JSON from prose before
— but because "reliable structured output" and "fragile heuristic" are not the same
quality of building block, and reliability is what you need to build on top of.
