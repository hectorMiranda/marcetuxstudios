---
layout: post
title: "ES6 promises replacing callback chains"
date: 2015-02-04
author: marcetux
tags: [javascript, es6, async, frontend]
---
The matching module has a sequence: fetch profile from Couchbase, run the filter,
persist the result, notify via SQS. Callback-style that's four levels of nesting by the
time you handle errors at each step. I rewrote it using native ES6 `Promise` — Babel
polyfills it for the browsers that don't have it yet — and the nesting collapsed to a
flat chain.

The key insight I kept having to relearn: a promise represents a value that will exist
eventually, not a callback you register. So `fetch(id).then(filter).then(persist)`
reads as a pipeline, and each step returns either a value or a new promise — the chain
handles the sequencing. Error handling falls to a single `.catch()` at the end of the
chain rather than the same if-err-return pattern duplicated at each level. That alone is
worth the change.

What still requires care is parallel operations. `Promise.all([a, b, c])` runs a, b, c
concurrently and resolves when all three finish — but if any one rejects, you get the
first rejection only, not all of them. That's usually fine and occasionally surprising.
For the matching pipeline it doesn't matter — the steps are inherently sequential —
but I've already hit a case where I wanted all three failures logged, not just the first.
`Promise.all` is the right primitive; knowing when it isn't is the part you learn after.

*Update: One more pattern worth noting after a week of use — wrapping each `.then()`
handler in a try/catch is redundant; any throw inside a promise handler automatically
becomes a rejection and flows to the nearest `.catch()`. The moment I stopped defensive-
coding inside individual handlers and trusted the chain, the code got meaningfully
shorter. The chain is the error boundary.*
