---
layout: post
title: "Logging you'll actually read"
date: 2012-11-26
author: marcetux
tags: [dotnet, logging, observability, ops]
---
Spent a chunk of the long weekend untangling a production hiccup, and the thing that
made it slow wasn't the bug — it was logs that told me everything except what I
needed. So, notes to self on logging that earns its disk space.

**Levels mean something.** ERROR is "a human should look"; WARN is "this is
recoverable but suspicious"; INFO is the heartbeat; DEBUG is firehose you turn on
deliberately. When everything is logged at INFO, nothing is.

**Log context, not prose.** "Failed" is useless. "Failed to aggregate customer=4821
range=2012-11-01..30 after 3 retries" lets me reproduce it. I'm leaning on NLog with
structured-ish messages and a correlation id threaded through a request so I can
follow one customer's path through the logs instead of reading the whole haystack.

The test: could someone who isn't me debug a 2am incident from these logs alone? If
not, I'm writing them for the wrong reader.
