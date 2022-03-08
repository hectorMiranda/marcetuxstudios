---
layout: post
title: "Testing Casper contracts without a live node"
date: 2022-03-07
author: marcetux
tags: [casper, testing, rust, smartcontracts, devops]
---
The naive test loop for smart contracts is: compile to WASM, deploy to a local node,
query state, decide if it worked. That loop takes thirty to sixty seconds for anything
non-trivial and gives you very little information when it fails. For logic-heavy
contracts it's the wrong loop, and CasperLabs ships a test framework called
`casper-engine-test-support` that cuts the latency dramatically.

The test support crate bundles the contract execution engine — the same WASM runtime
the node uses — in a library you can call from a normal `cargo test`. You load your
compiled WASM, call its entry points with typed arguments, and inspect the resulting
global state, all within a regular Rust test function. No node running, no RPC, no
deploy hashes to query — just `assert_eq!` on the state you expect. The feedback loop
drops from a minute to a second.

The limitation is that the engine test environment is not the full node — you won't
catch network-level issues or multi-contract interactions that span multiple sessions
within a single deploy. For those you still need the local node. But the engine tests
catch the logic errors, which are the majority of the bugs. Keeping the unit test layer
fast and the integration layer honest about what it's testing is the same discipline
as any other kind of backend code; the blockchain doesn't change the principle, only
the vocabulary.
