---
layout: post
title: "February notes"
date: 2020-02-28
author: marcetux
tags: [meta, retrospective]
---
February was a month of taking things that were working-ish and making them
work properly. Blazor Server has been in production long enough to have real
opinions on the connection model; the Helm charts stopped requiring their author
to explain them; `IHttpClientFactory` killed a socket exhaustion problem I'd been
dismissing as "intermittent." The thread is the same: things that are fine right
up until they're not, and then you read the docs you should have read at the start.

The home lab got more serious this month. k3s is now running the whole sensor stack,
and it restarted Mosquitto without me twice already. That's the cluster earning its
keep. The InfluxDB node-pinning is still a gap — Longhorn is on the list — but
the immediate brittleness is gone.

March is when I'll be thinking about Blazor WebAssembly. The GA preview is close
enough to experiment with. And there are conversations at work about what distributed
teams might look like if travel-related disruptions hit harder this spring — nothing
concrete, but the infrastructure work we've done on remote-friendly pipelines is
starting to feel prescient.
