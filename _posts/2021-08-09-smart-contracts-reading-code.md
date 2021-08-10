---
layout: post
title: "Reading smart contract code for the first time"
date: 2021-08-09
author: marcetux
tags: [blockchain, solidity, web3, learning]
---
Spent a weekend reading Solidity. Not writing it — reading other people's contracts
on Etherscan, specifically a few of the ERC-20 token implementations and one of the
smaller NFT minting contracts that had been audited and published. The goal was to
understand what "code running on a blockchain" actually means from an engineering
standpoint, not from a pitch deck.

The thing that hits you first is the immutability constraint. A deployed smart
contract's bytecode doesn't change. Ever. There are upgrade patterns — proxy
contracts that delegate calls to a replaceable implementation contract — but the
original contract address and its behavior are fixed the moment you deploy it. The
upgrade proxy is its own trust assumption: now you're trusting that the admin of
the proxy won't swap in a malicious implementation. Smart contract security is
largely about what you've committed to in immutable code versus what you've left
addressable by someone.

The gas model is the other piece that reorganizes how you think about code. Every
operation has a cost in gas: storage writes are expensive, reads are cheap,
computation is priced per opcode. Algorithms you'd write without thought in C# —
iterating over an array, doing string manipulation, calling a library — have
real dollar costs here. The design constraint isn't "is this fast?" but "is this
cheap enough that the transaction completes at all?" A different discipline, and
one I find genuinely interesting. Something is forming.
