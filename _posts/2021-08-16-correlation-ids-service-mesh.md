---
layout: post
title: "Correlation IDs across service boundaries"
date: 2021-08-16
author: marcetux
tags: [observability, tracing, architecture, dotnet]
---
The distributed trace story I improved in April fell apart at the boundary between
our services and a third-party payment processor: the correlation ID we passed in
the `X-Correlation-Id` header didn't come back in the processor's logs, so when
a payment failed we could trace up to the point we sent the request and no further.
The correlation ID scheme was correct internally; it stopped at a wall we didn't
control.

The fix that worked: the processor's API does return a unique transaction reference
in the response body. We extract that reference, add it as a tag on the outbound
span, and also write it to a local correlation table — `{our_trace_id, processor_ref_id, timestamp}`.
When an investigation hits the wall, we pivot to the processor reference and use
their support portal to pull the processor-side trace. Two systems, two trace
spaces, one join table that bridges them.

The broader principle: you can't force a third party to adopt your observability
infrastructure, but you can build the bridge. Every integration should have a
reference — their identifier for the operation — stored alongside your own identifier
for that operation. The join is cheap to make at call time and expensive to
reconstruct after the fact. Design the bridge when you design the integration, not
when the incident tells you it's missing.
