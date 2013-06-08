---
layout: post
title: "TLS termination at the CDN edge and what that actually means"
date: 2013-06-08
author: marcetux
tags: [security, https, cdn, networking, performance]
---
Following the HTTPS audit from last week, I dug into how TLS actually works in a CDN
context, because "the CDN handles HTTPS" is a phrase people say without always knowing
what it means. The short version: TLS terminates at the edge, meaning the CDN PoP
decrypts the traffic before it hits the origin. The browser is talking securely to the
edge, not to the origin server.

That has real implications. The connection from edge to origin can be plain HTTP or a
separately established TLS connection — depending on how you configure it. Most origins
default to plain HTTP between edge and origin because it's simpler and the edge-to-origin
link is on a "trusted" network. But trusted private network is not the same as encrypted
private network. If you want end-to-end encryption, you need TLS on the edge-to-origin
leg too, which costs more CPU and requires a valid certificate on the origin.

For the portal's customer-facing API, I'm adding a certificate on the origin and
configuring the edge to connect via HTTPS. The performance hit is measurable but small —
TLS handshakes are fast with session resumption, and the handshake only happens on new
connections, not on every request. The security property I'm buying is that origin
traffic is encrypted even if the edge-to-origin path isn't as trusted as we assume it
is this week. Post-PRISM week seems like a good time to stop assuming.
