---
layout: post
title: "Getting a Casper node running locally"
date: 2022-01-11
author: marcetux
tags: [casper, blockchain, devops, rust]
---
Before I can write or deploy contracts, I need a place to deploy them, and running
a local Casper node turned out to be more involved than standing up a Docker container.
The casper-node binary — written in Rust — is the actual consensus and execution engine.
Getting it into a state where you can submit transactions against it locally is a half-
day of configuration that the official docs underexplain.

The key pieces: the node needs a chainspec (a YAML config describing the initial
chain state), a set of validator keys, and a genesis account. Once it's running you
talk to it over a JSON-RPC endpoint. The tooling for that is the Casper client CLI,
which wraps the raw RPC calls into subcommands — `get-state-root-hash`, `put-deploy`,
`get-deploy` — and translating what's happening under the hood from those commands
is the actual learning. A "deploy" is Casper's term for a transaction that includes a
payment session (how much CSPR the caller is willing to spend on gas) and a session
body (the WASM that actually runs).

The mental model I'm settling on is: the node is the engine, the chainspec is the
rules, and the client is the RPC wrapper that lets you inspect state and submit work.
Once that clicked, the setup is logical. The local node isn't production — it's a
single-node test environment — but it's enough to deploy contracts and watch state
change in the global state trie.
