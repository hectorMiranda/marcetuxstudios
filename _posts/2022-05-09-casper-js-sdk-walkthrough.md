---
layout: post
title: "The Casper JavaScript SDK in a dApp frontend"
date: 2022-05-09
author: marcetux
tags: [casper, javascript, sdk, dapp, frontend]
---
The Rust side of contract development has a coherent workflow: Cargo, the contract
crate, the engine test support library. The JavaScript side — the SDK that dApp
frontends use to interact with the chain — is younger and has more rough edges, which
is accurate for where Casper is in its ecosystem maturity. That said, the core it needs
to have is there: constructing and signing deploys, querying named keys, talking to
the RPC endpoint.

The most useful part of the SDK for a frontend developer is `DeployUtil` — the set of
helpers that lets you describe a contract call in terms of entry point name and typed
arguments and get back a serialized deploy structure ready for the user's wallet to
sign. The types have to match what the contract expects on the Rust side exactly, which
means understanding Casper's CLType system (the binary type encoding for deploy args).
A `u64` on the Rust side becomes `CLValue.fromT(BigInt(42), CLTypeU64)` in JavaScript.
That mapping is where most of my integration bugs have lived.

The query side is simpler: `CasperServiceByJsonRPC.getStateRootHash()` followed by
`CasperServiceByJsonRPC.getBlockState(stateRootHash, accountKey, path)` gets you a
named key's current value. It's verbose but it maps exactly to the RPC calls happening
under the hood, which means the debugging story is clear: if the query is failing, you
know exactly which RPC method to inspect.
