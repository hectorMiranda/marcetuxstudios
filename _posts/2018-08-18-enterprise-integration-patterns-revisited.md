---
layout: post
title: "Enterprise integration patterns and why the old book still applies"
date: 2018-08-18
author: marcetux
tags: [enterprise, integration, architecture, patterns, messaging]
---
Two months of integrating airline, hotel, and car GDS systems has me rereading the
Hohpe and Woolf enterprise integration patterns book, which came out in 2003 and
describes patterns we are still implementing with different tools. The Message
Channel, Message Router, Content Enricher, Dead Letter Channel — the names have
changed in some cases, the platforms have evolved, but the problems are structurally
identical to what a Mulesoft flow or an Azure Service Bus topology is solving today.

The pattern that keeps appearing in the CTM integration work is the **Correlation
ID**. When a booking request fans out to an airline GDS, a hotel API, and a car
rental service, each in parallel, and the responses come back asynchronously on
different channels, you need something to match the responses to the original request.
We were using a generated booking reference as the correlation key, but it wasn't
propagated consistently — some responses came back without it, and we were matching
on a combination of fields that wasn't guaranteed unique.

Fixing the correlation ID propagation is the kind of change that looks trivial on a
diagram and requires careful surgery in the actual system. Every integration point
must write the ID on the way out and read it on the way in. Any hop that strips
headers — a proxy, a message queue that doesn't forward custom attributes — breaks
the chain. That's not a new problem. Hohpe and Woolf described it in 2003.
