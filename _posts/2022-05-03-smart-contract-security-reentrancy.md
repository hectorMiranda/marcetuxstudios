---
layout: post
title: "Smart contract security and why reentrancy is the classic mistake"
date: 2022-05-03
author: marcetux
tags: [casper, smartcontracts, security, web3]
---
The DAO hack in 2016 introduced most of the industry to reentrancy, and it's still the
first thing you audit for in any smart contract that moves value. The pattern is simple
and the mitigation is a single discipline: update state *before* making external calls,
not after. When a contract calls out to another contract before finishing its own state
update, the callee can call back into the caller while the state is still in an
intermediate, exploitable condition.

On Casper the risk surface is different than on Ethereum. Casper's execution model
processes entry points sequentially within a single session, and cross-contract calls
are synchronous — there's no async callback mechanism of the sort that created the
original DAO vulnerability. But the principle still applies whenever a contract reads
state, does an operation, then writes state: the write should come first if the
subsequent operation could cause a re-entry.

The audit discipline I've internalized is: for any entry point that transfers value or
modifies a balance, draw the state machine on paper and ask where an adversarial callee
could interrupt the transition. If the state at any intermediate step is exploitable,
reorder to close the window. It's the same principle as optimistic locking in a
database — assume contention, design so the window of vulnerability is zero, and don't
rely on "nobody would do that" as a security argument.
