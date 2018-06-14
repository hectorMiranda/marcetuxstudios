---
layout: post
title: "Mulesoft and API-led connectivity in an enterprise context"
date: 2018-06-13
author: marcetux
tags: [mulesoft, enterprise, integration, api, architecture]
---
CTM runs Mulesoft for its GDS integrations and I've spent the last two weeks learning
it properly rather than reading around it. The "API-led connectivity" pattern
Mulesoft promotes — System APIs, Process APIs, Experience APIs as three tiers — is
something I initially dismissed as vendor-speak, and I was partially wrong. The
three-tier idea is a real architectural pattern that happens to be what Mulesoft sells
training for.

The System API layer is the translation layer: it wraps a GDS or airline SOAP service
and gives you a consistent REST interface regardless of what's underneath. The Process
API orchestrates across multiple System APIs to assemble a business capability — book
a trip — that requires talking to airline, hotel, and car in sequence. The Experience
API shapes the Process API response for a specific consumer: the mobile app sees one
payload, the admin portal sees another. The principle is the same as the adapter
pattern, just with names that match an enterprise conversation.

Where Mulesoft earns its complexity: the Anypoint Platform manages discovery,
security, and analytics across all your APIs in one place, which is genuinely useful
once you have twenty APIs and need to know which ones a client is calling and at what
rate. Where it costs you: the DataWeave transformation language has a steep learning
curve, the local development story involves a heavy IDE, and the licensing is not
subtle. For three integrations, probably overkill. For thirty, probably necessary.
