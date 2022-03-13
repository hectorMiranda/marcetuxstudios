---
layout: post
title: "dApp architecture on Casper: what the frontend actually does"
date: 2022-03-12
author: marcetux
tags: [web3, dapp, casper, frontend, architecture]
---
A decentralized application's frontend has a different job than a normal web frontend.
In a normal web app the frontend calls your API; the API owns the state; the API
enforces the rules. In a dApp the rules are enforced on-chain, the state is the global
blockchain state, and the frontend's job is to help users construct and sign deploys —
the on-chain transactions — and then show them the current state of the world as read
from the chain.

On Casper that means the frontend integrates with the Casper Signer browser extension,
which holds the user's key. The app doesn't touch the private key. It constructs a
deploy object — describing which contract entry point to call with which arguments —
serializes it, passes it to the Signer for the user to approve and sign, and then
submits the signed deploy to a node. The app is a UX wrapper around the user's
sovereign signature. That's a genuinely different trust model than "user logs in, we
own the session."

The state-reading side is simpler: the Casper JavaScript SDK provides methods to query
named keys from the global state trie, and since the state is public and content-
addressed you can read it without any authentication. The complication is latency —
you're querying a node for the current state root, then querying within that root. For
a read-heavy dApp you typically cache aggressively and accept that your view lags chain
time by a few seconds. The chain is the source of truth; the frontend is eventually
consistent with it.
