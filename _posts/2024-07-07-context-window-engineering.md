---
layout: post
title: "Context window engineering"
date: 2024-07-07
author: marcetux
tags: [llm, ai, architecture, prompts, performance]
---
A 128k context window sounds like it solves retrieval — just stuff everything in.
That's the wrong mental model for a few reasons, and the client who tried it last
month found out the expensive way. Tokens cost money and they cost latency; a full
128k context on every request is a throughput and cost problem. More importantly,
model performance on long contexts degrades toward the middle of the window —
the "lost in the middle" phenomenon, where facts buried in the center of a large
prompt are reliably less recalled than facts near the edges.

The right approach is still retrieval, not expansion. Large windows are useful for
specific things: very long single documents, multi-turn conversations where prior
turns are relevant, structured tasks where the full schema needs to fit. They're not
a substitute for good chunking and retrieval in a multi-document system. The
discipline is thinking about what the model actually needs per request, not how
much it could technically receive.

The practical pattern for conversational systems: a sliding window over conversation
history, a retrieval step for relevant documents, and a separate summarization of
older context that can't fit in the window. The summary goes near the top where
recall is high. The most recent turns go near the bottom. Retrieved facts go
adjacent to the query. Putting things in the right position in the window matters;
it's not a bag. Context window engineering is an unglamorous discipline that pays
back in quality and in bills.
