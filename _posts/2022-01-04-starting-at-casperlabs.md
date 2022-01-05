---
layout: post
title: "Starting at CasperLabs"
date: 2022-01-04
author: marcetux
tags: [career, blockchain, rust, casper]
---
New year, new job, genuinely new domain. I joined CasperLabs in January to work on
the Casper network — a proof-of-stake blockchain — and the first week has already made
it clear that the mental model I carry around from five years of REST APIs and Azure
pipelines will not survive intact. The stack here is Rust and WebAssembly, the
deployment target is a decentralized network of nodes, and "rollback the bad deploy"
is not an option once a smart contract lives on-chain.

The thing I keep bumping into is that blockchain engineering reverses a lot of
assumptions. In a normal service you can patch the server, restart a process, revert
a migration. On Casper, state is global and immutable — once a transaction executes,
the result is in the ledger forever, replicated across hundreds of nodes who don't
trust each other. That's not a constraint to work around; it's the whole point, and
it means contract design has to be right before it ships in a way that backend code
usually doesn't.

The Rust requirement is less surprising to me than I expected. I'd been poking at the
language since late last year and the ownership system starts to make sense once you've
fought it enough times. Here it's non-negotiable — smart contracts compile to WASM, and
the safety guarantees Rust gives you at compile time matter a lot more when the "runtime
exception" costs real money. January resolution: get the ownership model deep enough
that I stop fighting the borrow checker and start hearing what it's telling me.
