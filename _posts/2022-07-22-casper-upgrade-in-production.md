---
layout: post
title: "Shipping a contract upgrade on the Casper testnet"
date: 2022-07-22
author: marcetux
tags: [casper, smartcontracts, devops, blockchain]
---
Theory is one thing; shipping an actual upgrade to a contract on the Casper testnet
while the contract has live state is another. We did our first production-path upgrade
this month, and the discipline of working through it carefully in a staging environment
before touching testnet is the part I'd enforce as a hard rule on any future project.

The upgrade deploy structure in Casper sends the new contract WASM alongside a session
that registers the new version under the existing contract package hash. The key is
that you're not replacing the old version — you're adding a new version to the package.
Existing callers that have stored a reference to a specific version continue calling
it. You then update a routing named key (a "current version" pointer in the contract's
named keys) to direct new calls to the new version. The migration of any existing data
happens in the new version's call path, lazily, not in a big-bang upgrade transaction.

What almost went wrong: the new version expected a field in the stored dictionary that
the old version had never written, and the first call against a legacy account would
have panicked. The fix was an `Option` wrapper on the read with a default fallback for
missing entries. Testing against a copy of the testnet state caught it; catching it in
production would have required an emergency further upgrade. The cost of a proper staging
environment is a fraction of the cost of convincing users their tokens are safe after
an on-chain panic.
