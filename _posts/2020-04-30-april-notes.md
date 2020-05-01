---
layout: post
title: "April notes"
date: 2020-04-30
author: marcetux
tags: [meta, retrospective]
---
April was the observability and process month. Splunk saved searches became
documented infrastructure instead of tribal knowledge. Correlation IDs are now
threaded through all four services in the payment flow, which made the first
incident after the change embarrassingly fast to diagnose. The distributed tracing
system is still on the backlog, but the correlation primitive does most of the work
for a fraction of the setup cost.

Remote incident response is getting better. Incident commander rotation is working —
the channel is calmer, the status updates are more useful than "checking now," and
we've had two incidents this month where a relatively junior engineer led coordination
effectively because the process is explicit enough to follow. That's the sign a
process is real: it works without the person who designed it running it.

The home lab is finally done embarrassing me. Longhorn means the data survives a
node going down; the Grafana dashboard is the first thing I look at in the morning.
May: Blazor WebAssembly is supposed to go GA. I've been running the preview for two
months and I'm ready to ship something with it.
