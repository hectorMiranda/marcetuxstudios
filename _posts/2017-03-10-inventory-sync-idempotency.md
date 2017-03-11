---
layout: post
title: "Idempotency in the inventory sync pipeline"
date: 2017-03-10
author: marcetux
tags: [architecture, messaging, integration, reliability]
---
The inventory sync that pushes seller stock levels to Amazon and Walmart runs on a
schedule and on demand when a seller's warehouse system fires a webhook. Two things
happen frequently enough to matter: the scheduler and the webhook sometimes both fire
for the same seller within minutes of each other, and the channel APIs occasionally
accept a feed and then time out before returning the feed ID, causing us to retry a
feed we don't know landed.

Both problems dissolve if the operation is idempotent: sending the same inventory update
twice produces the same end state, not a doubled or inconsistent one. For us that meant
two things. First: a feed request that gets a timeout response gets retried with an
idempotency key — a deterministic hash of the seller ID, channel, and the timestamp of
the warehouse snapshot. If the feed already landed, the channel API returns the original
feed ID. If it didn't, a new feed is created. Same key, same result.

Second: before sending a feed, we check whether the computed delta is meaningfully
different from the last confirmed send. Sending zero-change inventory updates wastes API
quota and can reset marketplace metrics. The delta check filters that noise. Boring to
build, invisible when it works — but the absence of duplicate feeds in the audit log is
the thing I'm most quietly proud of in the pipeline.
