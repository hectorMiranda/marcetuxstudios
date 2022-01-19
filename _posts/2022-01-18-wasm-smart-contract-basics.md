---
layout: post
title: "Writing a first smart contract for Casper in Rust"
date: 2022-01-18
author: marcetux
tags: [rust, casper, wasm, smartcontracts, web3]
---
The "hello world" of Casper smart contract development is a simple key-value store
contract — write a named key to the global state trie — and even that small exercise
exposed how different the programming model is. The contract is a Rust crate that
compiles to WASM. The entry points are annotated functions marked with `#[no_mangle]`
so the casper-node runtime can find them by name after loading the WASM binary.

State in a Casper contract is not struct fields — it's named values written to the
global state trie via the `runtime` API. You call `runtime::put_key("my_value",
storage::new_uref(42).into())` and that value is now globally accessible by anyone
with the appropriate permissions. Reading it back is `storage::read_from_key("my_value")`.
The whole global state of the blockchain is one giant content-addressed trie, and your
contract's named keys are your slice of it. No database, no file system — just the trie.

The contract compilation target is `wasm32-unknown-unknown`, which is the bare-metal
WASM target with no OS abstractions. That means no file I/O, no threads, no network
calls — the contract can only interact with state through the `casper_contract` crate's
runtime API. Once I stopped trying to think of it as a normal Rust program and started
thinking of it as a pure function over the global state, the model became coherent.
