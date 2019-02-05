---
layout: post
title: "Mutual TLS between internal services"
date: 2019-02-04
author: marcetux
tags: [security, tls, mtls, architecture, banking]
---
Service-to-service authentication in a bank is not optional, and for a long time the answer was "same VNet, shared secrets in config files." The VNET was the trust boundary. I've been pushing to replace that with mutual TLS on internal calls, and the compliance team is finally receptive now that we have a framework for it.

Mutual TLS means both sides of a connection present a certificate. The payment service that calls the ledger service proves who it is, not just that it's inside the network. The ledger service rejects connections from anything it hasn't been configured to trust. You get cryptographic identity on every hop — no more "trust the IP" reasoning that falls apart the moment someone stands up something unexpected in the VNET. On AKS, we're using cert-manager to issue and rotate certificates automatically from our internal CA, which means the rotation operational burden is nearly zero once the setup is done.

The hard part was the mental shift in the teams. Developers who'd never thought about certificates suddenly had to understand what a client cert is and why the connection was refusing. I ended up writing a runbook that explains the mTLS handshake in terms of "you show your badge, I show my badge, we both check each other's HR records." Oversimplified but it got the concept across. The reward is an audit trail that's not "who could have been on that network" but "here is the signed proof of which service made each call."
