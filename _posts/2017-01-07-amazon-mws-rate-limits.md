---
layout: post
title: "Working within Amazon MWS rate limits"
date: 2017-01-07
author: marcetux
tags: [amazon, marketplace, api, architecture]
---
Amazon's Marketplace Web Service isn't shy about its limits — every operation type has
a request quota and a restore rate published in the docs, and if you blow past them you
get throttled hard. The early SolidCommerce code treated MWS like an internal API and
hit it as fast as it felt like, which worked fine until we were managing enough seller
accounts that the collective throughput tripped the per-account limits across the board.

The fix is a token-bucket per account per operation category. MWS publishes the restore
rate for GetOrder, ListOrders, GetFeed, etc.; you model each as its own bucket and drain
a token before each call, blocking if the bucket is empty rather than firing and eating
the throttle response. The nice thing about the consumer-per-queue layout we put in with
RabbitMQ is that controlling concurrency *is* controlling throughput — each consumer
burns one slot, so the pool size is the knob.

The tricky part is that MWS errors aren't consistent: some throttle responses come back
as HTTP 503, some as a specific error code in the XML body. You need to check both and
handle them the same way — jitter + exponential backoff, not a fixed retry. We log every
throttle event by account and operation now, and the dashboard has been illuminating.
The ones we thought were fast are just the ones we hadn't measured.
