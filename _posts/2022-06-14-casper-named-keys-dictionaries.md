---
layout: post
title: "Named keys vs. dictionaries in Casper storage design"
date: 2022-06-14
author: marcetux
tags: [casper, smartcontracts, storage, design, web3]
---
The two main storage primitives in a Casper contract are named keys and dictionaries,
and picking the wrong one for a use case doesn't just hurt ergonomics — it can be
the difference between a contract that's usable and one that's functionally broken at
scale. I picked the wrong one in my first token implementation and had to refactor.

Named keys are the contract's top-level namespace: a string maps to a `Key` — typically
a `URef` pointing to stored data. They're simple and fast to access, but they're part
of the account/contract's named-key namespace, which means they're enumerable and
capped in a practical sense. If you're storing one value per user (balances, allowances),
a named key per user turns into a namespace pollution problem once you have thousands
of users.

Dictionaries solve this: a dictionary is a named key pointing to a `URef` that itself
is the root of a hash-addressed sub-trie. You access entries by string seed — the
account hash, typically — and the runtime handles the sub-addressing. The lookup is
slightly more verbose (`storage::dictionary_get(dict_uref, &account_str)`) but scales
without littering the contract's top-level namespace. For anything indexed by user or
token ID, the dictionary is the right answer. The named key is for the contract's own
configuration: total supply, contract version, admin key. Get that separation right
early and the upgrade story stays manageable.
