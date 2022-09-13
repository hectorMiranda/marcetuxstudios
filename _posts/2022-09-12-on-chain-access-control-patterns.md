---
layout: post
title: "On-chain access control: admin keys and role patterns on Casper"
date: 2022-09-12
author: marcetux
tags: [casper, smartcontracts, security, access-control, web3]
---
Every contract that can be configured or upgraded needs access control. The naive
implementation is a single admin account hash stored at deployment — only this account
can call the admin entry points. That's simple and often correct, but for contracts
that need to separate concerns — a mint role, a pause role, an upgrade role — the
single-admin model becomes a liability because any key compromise has maximum blast
radius.

The role pattern I've implemented on Casper stores a dictionary per role, keyed by
account hash, with a boolean value. Granting a role is setting the dictionary entry to
true; revoking it is setting it to false (or removing the key). An access-controlled
entry point calls `has_role(caller, "minter")` at the top, reverts immediately if the
check fails, and does its work otherwise. The admin account's job is just to manage
role grants, not to be the bottleneck for every privileged operation.

The upgrade story for access control deserves its own attention: the role dictionary
is part of the contract's stored state, which means it survives contract upgrades by
design. You add a new role in the new contract version; the existing role assignments
carry forward without any migration. That's one of the situations where Casper's
upgrade-as-new-version model is cleanly superior to an in-place modification — the
immutable history of who had which role at which point is preserved in the chain
history even though the active roles are just the current state.
