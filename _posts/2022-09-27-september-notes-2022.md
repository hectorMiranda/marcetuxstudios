---
layout: post
title: "September notes"
date: 2022-09-27
author: marcetux
tags: [meta, retrospective, rust, casper, homelab]
---
September is the first month where the contract library work paid a visible dividend
at the day job. Three contracts pulling from a shared crate, argument parsing errors
caught once in the library tests instead of three times in contract tests, consistent
error types across the system so the indexer can handle all three contracts with the
same error deserialization logic. The work to extract the library is almost invisible
now in the form of things that don't break.

The access control post is a topic I'd been meaning to write for a while because the
pattern comes up in every contract project and the naive single-admin design is one I've
seen go wrong in others' post-mortems. The Casper role model isn't novel — it's roughly
the same as OpenZeppelin's AccessControl on EVM chains — but implementing it in Rust
with the Casper storage primitives has its own texture.

The home lab upgrade went smoothly enough that it's not worth dwelling on, which is
the best possible outcome for infrastructure maintenance. The monitoring dashboards
came back up, the InfluxDB data didn't flinch, and the cluster is running on a version
I can find recent CVE information for. Boring Saturday, good outcome.
