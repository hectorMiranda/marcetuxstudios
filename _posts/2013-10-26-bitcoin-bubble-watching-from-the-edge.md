---
layout: post
title: "Bitcoin at $200 and watching traffic patterns from inside a CDN"
date: 2013-10-26
author: marcetux
tags: [cdn, performance, caching, bitcoin, http]
---
Bitcoin crossed $200 this week and the traffic patterns to the crypto-adjacent properties
on the CDN are noticeable in the dashboards I maintain. Not because we have many of
them, but because price volatility drives activity spikes that show up clearly in the
edge metrics: requests jump when prices move sharply, which makes intuitive sense —
everyone checks the price at the same moment.

From a CDN caching perspective, the price-feed discussion from earlier this month plays
out in real time. Properties that set reasonable `Cache-Control` headers handle the
spike with edge-served responses; the origin barely notices. Properties that set no
caching or explicitly prohibit it (`Cache-Control: no-store`) forward every request
to the origin, and the origin CPU spikes with the traffic. Two categories of customer:
one sees a smooth curve in the edge metrics, the other pages their on-call at 2 AM.

The thing I keep telling anyone who asks is that you can't add caching in a crisis.
The header has to be in the response before the spike; the TTL has to have been
tested before the load is real. Caching is an architectural decision you make at
design time, not an emergency response. The Bitcoin volatility is just making the
existing caching decisions visible in a way that low-traffic normal days don't.
