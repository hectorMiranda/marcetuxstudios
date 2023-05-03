---
layout: post
title: "Token limits and what context windows actually mean"
date: 2023-05-02
author: marcetux
tags: [llm, ai, architecture, rag]
---
The phrase "context window" appears in every LLM explainer and most of them treat it
as a capacity number — "this model takes 8,000 tokens." That's not wrong, but it
obscures the more useful frame: the context window is the total working memory of a
single inference call. Everything the model knows about your specific problem has to
fit in there — your system prompt, the conversation history, the retrieved documents,
and the question. The model has no memory between calls unless you put it in the
window.

A token is roughly ¾ of a word in English prose, about 1 character in code. Eight
thousand tokens is about 6,000 words, or a small chapter, or a few hundred lines of
reasonably dense code. GPT-4's 32k variant is about 25,000 words — long enough for a
meaningful slice of a codebase. The Anthropic models announced 100k, which is a
different category: you can put a full book in there.

The engineering consequence is that everything you do to "give the model context" is
constrained by this number. RAG exists largely because context windows used to be
small — you couldn't fit the whole knowledge base, so you retrieved the relevant
pieces. As windows grow, the retrieval architecture changes: you retrieve less, you
stuff more. But even at 100k, you still have to decide what's worth the tokens. The
budget is larger, not infinite.
