---
layout: post
title: "NFTs from an engineering angle"
date: 2021-07-09
author: marcetux
tags: [blockchain, web3, architecture, opinion]
---
I've been watching the NFT frenzy from a seat that's curious but not converted.
Half my feed is people declaring that ownership of digital goods is solved forever;
the other half is people pointing out that the NFT is usually a hash that points
to a URL that can 404. Both camps are right and talking past each other, and the
engineering reality is somewhere between the magic and the scam.

What the NFT actually is: a record on a public ledger that says "this wallet
address owns this token." The token is a number. The metadata — what the token
"is," usually a JPEG or a 3D asset — lives somewhere else, often IPFS, often a
centralized server that the issuer controls. The smart contract that mints the
token and records ownership is permanent; the thing the token points to is only as
permanent as where you chose to host it. That's a meaningful gap between the
marketing ("own this forever on the blockchain") and the mechanism.

The piece that's genuinely interesting to me is the contract programmability. A
smart contract that routes a royalty payment to the original creator on every
secondary sale is a real thing that doesn't require a lawyer or a payment processor
to enforce — it's in the contract code, it runs automatically, it's auditable. That
primitive has applications beyond JPEG ownership. The hype obscures the technical
novelty, which is a shame; the programmable-escrow and provenance-tracking use
cases deserve a less frothy environment to mature in.
