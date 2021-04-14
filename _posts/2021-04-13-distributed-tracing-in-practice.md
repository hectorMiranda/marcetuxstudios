---
layout: post
title: "Distributed tracing beyond the hello-world demo"
date: 2021-04-13
author: marcetux
tags: [observability, tracing, azure, architecture]
---
We've had Application Insights installed for a year and traces "working" in the
sense that requests show up in the transaction search. What we didn't have was
traces that were actually useful during an incident, which is a different bar.
The demo shows you a waterfall of spans. The incident shows you a wall of spans
for 200 different request types and no easy way to isolate the one that's slow.

The gap is instrumentation discipline. Out-of-the-box tracing records HTTP calls
and database queries automatically, which is a good start. It won't record what
matters most for your specific service: the fact that a pricing decision involved
rule engine X, or that a customer lookup hit the fallback data source, or that
a message was received from a specific queue partition. That context lives in
custom spans — `using var activity = activitySource.StartActivity("PricingEngine")` —
added where the code makes interesting decisions.

After a quarter of adding custom spans to the ten most painful investigation paths,
the traces tell a story instead of listing HTTP calls. When pricing is slow, the
trace now shows which rule set took 800 ms; we can paste the trace ID into Slack
and two people can look at the same causal chain. The investment is additive and
indefinitely composable — you add spans where investigations keep going blind, and
eventually the trace surface-area matches the investigation surface-area. That's
when observability becomes a culture instead of a tool.
