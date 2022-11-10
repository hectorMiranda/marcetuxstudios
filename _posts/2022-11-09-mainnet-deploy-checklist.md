---
layout: post
title: "The checklist before a Casper mainnet contract deployment"
date: 2022-11-09
author: marcetux
tags: [casper, smartcontracts, devops, production, blockchain]
---
We're approaching mainnet deployment on the project I've been working on all year, and
the pre-deploy checklist I've assembled is longer than anything I've written for a
traditional backend service. The stakes are different: a bug in a database migration
can be rolled back; a bug in a contract entry point that controls value cannot. The
checklist is the artifact that makes the process repeatable and the reviewers accountable.

The checklist has four sections. First: code review, with specific eyes on the security
patterns — reentrancy order, access control completeness, correct CLType encoding for
every argument type, no panics reachable in normal operation. Second: engine test
coverage, with every entry point covered under both happy-path and adversarial
arguments. Third: testnet verification, running the full user journey against the
current testnet state, including an upgrade from the previous deployed version if one
exists. Fourth: deployment verification, confirming after the mainnet deploy that the
expected named keys exist, the entry points respond to queries, and a smoke-test
transaction succeeds.

The checklist is aggressive about what "done" means for each item. "Code reviewed" is
not "I looked at it" — it's "a second engineer reviewed it and signed off." "Testnet
verified" is not "I ran one deploy" — it's "I ran the full sequence and the state looks
right." Checklists only work if the criteria are specific enough to be falsifiable.
"Looks good" is not a criterion; "named key `total_supply` returns a u64 equal to the
initial mint amount" is.
