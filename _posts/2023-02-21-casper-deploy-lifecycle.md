---
layout: post
title: "The Casper deploy lifecycle in detail"
date: 2023-02-21
author: marcetux
tags: [casper, blockchain, rust, smart-contracts]
---
Something I had to learn by reading the node source rather than any documentation:
the life of a deploy from client submission to finality on the Casper network. Getting
this wrong — submitting a deploy and thinking it's "done" after the RPC responds OK
— caused me a support headache in December that I don't want to repeat.

A deploy moves through several states. The node accepts it into its pending pool (the
RPC `put_deploy` returns here). It gets included in a block when a proposer selects
it. The block gets finalized once enough validators have signed it, which takes
multiple rounds of the Highway consensus protocol. Only after finality can you trust
the execution result — and even "finalized" means you need to check the deploy's
`execution_result` field, because a finalized block can contain a failed deploy. The
node accepted it; that doesn't mean the contract didn't revert.

The discipline this forces: always poll with `get_deploy` until the execution result
exists and is a success, not until the HTTP call returns. And set a timeout with a
real error, not an infinite retry loop. Distributed systems have a way of leaving
clients in "I don't know yet" indefinitely unless you decide how long you're willing
to wait.
