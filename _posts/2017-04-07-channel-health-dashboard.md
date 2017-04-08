---
layout: post
title: "Building a channel health dashboard for the ops team"
date: 2017-04-07
author: marcetux
tags: [dotnet, angular, monitoring, solidcommerce]
---
The ops team at SolidCommerce spends too much of their morning finding out which channel
connections are broken by checking the support queue instead of a dashboard. The
signals exist — feed submission timestamps, last-successful-sync times, API error
rates per seller per channel — but they live in separate tables and nobody had built
the view. April project: build the view.

The backend is a .NET Core API endpoint that aggregates the last 24 hours of channel
activity per seller. The query is straightforward — group by seller, channel, and hour
bucket; compute success rate and last activity — but the result set is potentially a
few thousand rows, so I added server-side pagination and a materialized view that
refreshes every ten minutes rather than computing it on demand. The ten-minute lag is
fine for an ops check; it's not a real-time monitoring tool.

The frontend is an Angular component with a color-coded status grid: green if the
channel synced successfully in the last two hours, yellow if the last sync was two to
six hours ago, red if it's been more than six. The ops team asked for one more thing:
a filter by error type, because "red" covers both "seller's API credentials expired"
and "our feed parser is broken," and those require different responses. Added that in
the second pass. The dashboard has been running for two weeks and has already caught
three seller credential expirations before they became support tickets.
