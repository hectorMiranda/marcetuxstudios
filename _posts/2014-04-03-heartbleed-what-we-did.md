---
layout: post
title: "Heartbleed and what we actually did about it"
date: 2014-04-03
author: marcetux
tags: [security, ssl, openssl, devops, incident]
---
The Heartbleed vulnerability was disclosed on April 7th and the rest of the week was
an incident. The bug is in OpenSSL's implementation of the TLS heartbeat extension —
a 64KB read beyond the intended buffer, accessible without authentication, that could
return whatever happened to be in memory at the moment: private keys, session tokens,
passwords. Two years of "just trust TLS" quietly imploded.

At a CDN the blast radius was significant. Edge nodes terminate TLS; they're exactly
the servers you least want leaking private key material. The first twelve hours were
inventory: which servers run which OpenSSL version, which services expose TLS endpoints.
Patching was fast once we had the list — package manager, restart, verify — but the
harder question was the certificates. If an attacker had the private key before the
patch, the patch alone didn't help. The right answer was revoke-and-reissue, which is
operationally expensive at scale.

The lesson I took wasn't about OpenSSL specifically. It was about assumptions. A critical
system had been running in production, trusted everywhere, and contained a remotely
exploitable memory leak. The code had been reviewed. Audits and peer review help but they
don't guarantee. Defense in depth — rate limiting, memory-safe layers, monitoring for
anomalous certificate usage — is what you have when a bug slips through everything else.
