---
layout: post
title: "How Casper handles smart contract upgrades"
date: 2022-02-03
author: marcetux
tags: [casper, blockchain, smartcontracts, rust]
---
One of the first things I asked when I joined CasperLabs was "how do we fix a bug in
production?" On Ethereum the honest answer to that question is a brutal detour through
proxy patterns and upgrade registries. Casper's model is different enough to be worth
explaining carefully, because it's the thing I'd want someone to have told me in week
one.

On Casper, a smart contract lives at a named key tied to an account. You can deploy
a *new version* of that contract — the chain keeps all prior versions indexed and
callable — and callers can be pointed at the latest or pinned to a specific version.
That's the versioning story: no bytecode mutation in place, just a versioned contract
package. The upgrade is an explicit deploy that registers a new entry point under the
same package hash. Old callers continue to work against whichever version they were
calling; new callers can be directed to the latest. It's closer to a versioned API
than a live patch.

The practical implication is that contract design has to think about data compatibility
from the start, because the global state written by v1 is what v2 reads. You can
upgrade the code; you cannot retroactively reshape the existing on-chain data. Get the
storage schema right early, and upgrades are mechanical. Get it wrong and the upgrade
path involves migration contracts, which are their own adventure.
