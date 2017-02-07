---
layout: post
title: "A Node microservice for feed transformation at SolidCommerce"
date: 2017-02-06
author: marcetux
tags: [nodejs, express, microservices, architecture]
---
Not everything at SolidCommerce is .NET. A new feed transformation service — taking
seller catalog data and mapping it into channel-specific schemas for Amazon and Walmart
— landed as a Node/Express process, and I want to write down why that was the right
call before the argument fades from memory.

The transformation logic is pure data manipulation: read a seller's product record,
apply a field mapping, emit a channel-specific XML or JSON structure. There's no
database, no session, no framework needed beyond "parse body, apply transform, write
output." Node's streams fit that shape well — you can pipe a large feed through the
transformer without holding it all in memory, which matters when a seller has 50,000
SKUs. The async I/O model isn't a novelty here; it's the reason the service can handle
twenty seller feeds concurrently on a single small instance.

The integration point is RabbitMQ — the .NET order pipeline and this Node service both
connect to the same broker. That's the architectural boundary that makes the technology
choice uncontroversial: the services don't call each other directly, so the choice of
runtime is local to each service. I'd push back on the teams that make this a values
debate. If a tool fits the problem, use it. The broker enforces the contract; everything
else is an implementation detail.
