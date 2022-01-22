---
layout: post
title: "The Casper deploy flow from CLI to on-chain"
date: 2022-01-21
author: marcetux
tags: [casper, blockchain, cli, devops]
---
Deploying a contract on Casper has more steps than `kubectl apply` and fewer than I
feared. The flow is: compile the Rust contract to WASM, create a signed deploy JSON
with the casper-client CLI, submit it to a node's RPC endpoint, then query back with
the deploy hash to see if it executed. That last step is where blockchain time teaches
patience — finality on Casper is probabilistic and takes a few seconds on the test
network, longer on mainnet.

The `casper-client put-deploy` command does most of the heavy lifting: you pass it the
WASM file path, the account key, the chain name, and the session args your contract
expects. It serializes everything into the Casper deploy format, signs it with your
private key, and sends it. The response is a deploy hash — that's your receipt. You
then call `get-deploy` with that hash until the execution result shows up. A successful
result includes the cost in motes (Casper's smallest denomination) and any errors.

The error messages in failure cases are terse by blockchain standards — an exit code
and a reason string from the WASM runtime — which means the local test loop matters.
I've been using the `cargo test` integration tests against the local node to catch
contract logic errors before hitting the test network. Getting the local feedback loop
tight is the same lesson as any other kind of server-side code; the network is just the
end of the pipeline, not the place to debug.
