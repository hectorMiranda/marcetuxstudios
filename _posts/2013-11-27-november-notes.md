---
layout: post
title: "November notes"
date: 2013-11-27
author: marcetux
tags: [meta, retrospective]
---
November felt like a holding pattern with focused bursts. The holiday traffic prep
consumed a chunk of work time — the CDN configuration audit is real engineering, even
if it doesn't produce new code. The SignalR reconnect and Redis Lua work from the start
of the month were the kind of infrastructure investments that only matter when something
goes wrong; they're now done before something went wrong, which is the right order.

Bitcoin crossed $400, which is entertaining to watch from inside a CDN. The traffic
patterns during price spikes are as educational as any architecture talk about
caching-under-load. The ambient sensor hub is now producing good DHT22 data after
fixing the timing interference — the bench readings and the ambient readings are
recorded in parallel, the comparison is something to do over a slower December week.

December: I expect the holiday traffic push to run through mid-month. The Verizon
acquisition of EdgeCast has been floating as a rumor for a few weeks and there was an
internal communication that was carefully noncommittal. I'm not sure what to make of
it, but keeping my head down on work quality seems like the right response to
uncertainty. Also: Bitcoin.
