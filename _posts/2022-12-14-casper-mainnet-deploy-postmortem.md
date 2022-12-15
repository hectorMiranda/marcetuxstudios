---
layout: post
title: "The mainnet deploy: what happened and what we got right"
date: 2022-12-14
author: marcetux
tags: [casper, blockchain, devops, production, postmortem]
---
We deployed to Casper mainnet last week. Everything went according to the checklist,
which is the outcome a checklist is supposed to produce. The post-deploy smoke test
found one issue: a named key we'd expected to query from the initial deploy hadn't
been created because a conditional in the contract only wrote it if a runtime argument
was provided, and our deployment transaction hadn't provided that argument. The key
existed in testnet because our testnet deploys had included the argument; the mainnet
deploy used a different script that didn't. The fix was a follow-up transaction; nothing
with user value was affected.

That near-miss is the most useful thing I can write about, because it illustrates
exactly where checklists fail: the item was "verify named keys exist" but the verification
script was checking against testnet state, not a fresh deploy on a blank mainnet account.
The environment divergence created a false positive in the verification step. The lesson
is not "write better checklists" — it's "the check must be environment-agnostic, verified
against the actual deployment target, not a proxy for it."

Everything else held. The access control, the token balances, the CEP-18 entry points,
the multi-sig — all verified clean. Six months of contract work, a proper test suite,
a pre-deploy checklist, and the discipline to actually run it. The one miss was the
one shortcut in the verification step. The shortcut is always where the miss lives.
