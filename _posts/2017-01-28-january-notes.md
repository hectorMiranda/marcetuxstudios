---
layout: post
title: "January notes"
date: 2017-01-28
author: marcetux
tags: [meta, retrospective]
---
First month of the year and SolidCommerce is already moving fast. The RabbitMQ order
pipeline landed in production and has been quietly absorbing the post-holiday volume
without drama, which is the best outcome: nothing broke, nobody paged me at 2am. The
Walmart channel went live at the end of the month and immediately surfaced the feed
error problem I wrote about — good to catch on day one rather than day thirty.

The Angular work is paying off in a way I can actually measure. The seller dashboard
components I rewrote to the strict one-direction model take a fraction of the time to
debug. The ones I didn't touch are still the ones with the weird state bugs. The pattern
works and I need to finish the migration instead of complaining about the old code in
standup.

Home side: Home Assistant is running and stable, and the MQTT integration with the ESP32
is next. I have sensors from a batch order waiting on my bench. February goal is to get
temperature and humidity from at least two rooms reporting into the HA dashboard, with
automations that actually do something useful rather than just light up a gauge.
