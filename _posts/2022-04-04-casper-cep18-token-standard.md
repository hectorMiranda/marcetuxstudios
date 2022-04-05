---
layout: post
title: "The CEP-18 token standard on Casper"
date: 2022-04-04
author: marcetux
tags: [casper, smartcontracts, tokens, web3, standards]
---
ERC-20 on Ethereum established the template for fungible tokens: a contract that tracks
balances, supports transfer and approve/transferFrom, and exposes a standard interface
so wallets and exchanges can interact with any token the same way. Casper has its own
equivalent — CEP-18 (Casper Enhancement Proposal 18) — and spending time with it this
month gave me a good view of both what the pattern looks like in Rust and where Casper's
model diverges from the Ethereum baseline.

The CEP-18 contract stores balances and allowances in the global state trie using
a dictionary per account, which is Casper's equivalent of a mapping in Solidity. The
transfer entry point reads the sender's balance, checks it covers the amount, decrements
the sender's entry, increments the recipient's, and writes both back in the same
execution — one atomic unit. There's no separate "commit" step because every entry
point execution is a transaction. The atomicity is free; the cost is the gas.

What differs from ERC-20 is the permission model: Casper has first-class access control
via account/contract permissions, so the approve/transferFrom pattern is less central.
A caller can be granted direct transfer rights to a named key without the approval
dance. That's cleaner in some scenarios and introduces a different set of design
decisions in others. The standard is still evolving, which is expected for a younger
network, and knowing why the Ethereum patterns exist is the best preparation for
adapting them to a different execution model.
