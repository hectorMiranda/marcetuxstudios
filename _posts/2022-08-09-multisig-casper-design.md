---
layout: post
title: "Designing a multi-sig scheme on Casper"
date: 2022-08-09
author: marcetux
tags: [casper, smartcontracts, security, multisig, web3]
---
Multi-signature authorization — requiring N-of-M key holders to approve an operation —
is a table-stakes security feature for any contract that controls significant value.
Casper has native multi-sig support at the account level, which covers a lot of use
cases, but the project this month needed it at the contract level with different key
sets for different operation types. That's a custom implementation.

The design is straightforward: the contract stores a list of authorized signers and a
threshold count. A multi-sig transaction is a two-step process — a proposer creates a
pending operation with a unique ID and the proposed parameters, then signers call an
`approve` entry point with the operation ID. When the approval count reaches threshold,
the contract executes the pending operation. Each signer's account hash is stored in
the approved-by list for that operation, and the contract verifies the caller's account
hash against the authorized list before recording the approval.

The sharp edge: `runtime::get_caller()` returns the account hash of the transaction
sender, which is what you want for tracking approvals. But you need to verify that
the caller is in the authorized signer set, which means the authorized set has to be
stored on-chain in a dictionary keyed by account hash — so the lookup is `O(1)` against
the trie rather than a linear scan through a stored vector. The first implementation
used a vector and a linear scan. The second uses a dictionary. The second is what ships.
