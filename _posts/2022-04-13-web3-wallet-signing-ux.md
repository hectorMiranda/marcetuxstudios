---
layout: post
title: "Web3 wallet signing and the UX problem it creates"
date: 2022-04-13
author: marcetux
tags: [web3, ux, dapp, casper, frontend]
---
Every time a dApp user wants to do something that writes to the chain, they see a
wallet pop-up asking them to approve and sign a transaction. From a security standpoint
this is the correct design — the user's key never leaves their wallet, and they
explicitly consent to every operation. From a UX standpoint it is a speed bump on every
meaningful action, and watching non-technical users try to navigate it is humbling.

The approval dialog typically shows a serialized deploy: entry point name, arguments,
payment amount in motes (Casper's denomination, where one CSPR is a billion motes),
and a chain ID to prevent replay attacks. A sophisticated user reads that and understands
it. Most users see a wall of hex and a number that doesn't map to any denomination they
recognize. The UX work of making that dialog legible — translating motes to CSPR, naming
the action in plain language, showing the expected gas cost in something human-readable —
falls on the dApp, not the wallet.

The honest assessment after a month of building frontends against this flow: the web3
mental model is genuinely different enough from normal web apps that user education is
unavoidable. The question is whether you front-load it in onboarding or let users
discover it confused in the middle of a transaction. Front-loading it is worse for
conversion and better for trust. I know which tradeoff I prefer, and it's the one that
doesn't leave people confused about where their money went.
