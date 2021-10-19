---
layout: post
title: "Reading about consensus: proof of work vs. proof of stake"
date: 2021-10-18
author: marcetux
tags: [blockchain, learning, distributed-systems, web3]
---
I've been going through the Ethereum documentation more systematically now that the
merge from proof-of-work to proof-of-stake has a real timeline. The consensus
mechanism is the part that took me the longest to understand from first principles
— not the high level ("miners compete to add blocks") but the actual mechanism that
makes it safe to not trust the participants.

Proof of work's security model is physical: the energy cost of rewriting history
grows with the history's length. To replace the last N blocks you have to redo the
work of all N blocks faster than the honest chain is growing, which at any realistic
hash rate becomes impossible past a small number of confirmations. The attack is
financially unattractive, not cryptographically impossible. The energy expenditure
is the fee you pay for having no trusted parties; it's a purposeful waste designed
to make honest participation cheaper than dishonest participation.

Proof of stake trades the energy expenditure for a slashing condition. Validators
lock up a stake of ETH and sign off on blocks. If a validator signs conflicting
blocks — the equivocation attack — the protocol destroys their stake. Security
comes from economic consequence rather than physical work. The interesting
engineering question is the validator set: how do you rotate validators fairly,
how do you handle validator clients that crash, how do you prevent stake centralization.
The distributed systems problems don't go away; they change shape. I'm taking notes.
