---
layout: post
title: "Smart contract auditing patterns in Rust"
date: 2023-07-17
author: marcetux
tags: [rust, casper, smart-contracts, security]
---
Part of the work at CasperLabs this quarter involved reviewing contracts written by
external teams before they deployed to mainnet. That's a code review with real money
on the line, which concentrates the mind. The patterns worth catching in Rust Casper
contracts are different from the Solidity vulnerabilities you read about in postmortems
— reentrancy doesn't apply the same way — but there are Rust-specific failure modes
that are genuinely dangerous.

The first class: integer arithmetic. Rust in release mode can silently wrap on integer
overflow, unlike debug mode which panics. Contract code that calculates a token balance
with an operation that could overflow in release is a real bug. The fix is explicit:
use `checked_add`, `checked_mul`, or switch to `u128` with explicit overflow checks.
The compiler won't help you here by default because overflow behavior is considered
valid in release builds.

The second class: key collision in the contract's key-value store. Casper's runtime
gives contracts a namespaced storage dict; if two fields are serialized to the same
storage key, one silently overwrites the other. The audit discipline is to verify that
every storage key is unique and constructed deterministically. A function that builds
a key from user input with insufficient namespacing will eventually produce a collision.
Neither of these is a Casper-specific problem — they're the general class of "the
happy path tests green and the edge case corrupts state" bugs that are hardest to catch.
