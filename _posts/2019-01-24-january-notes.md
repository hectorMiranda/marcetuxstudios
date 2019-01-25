---
layout: post
title: "January notes"
date: 2019-01-24
author: marcetux
tags: [meta, retrospective]
---
January at the bank was mostly laying foundations: YAML pipelines in version control where the delivery process is readable by humans, OpenAPI specs as contracts that exist before the implementation, idempotency keys so mobile clients can retry without fear. The connecting thread is making implicit agreements explicit. A pipeline in a database, a handshake contract living only in a team's memory, a "just don't double-tap" instruction to users — all of those are implicit, which means they're fragile.

The gRPC experiment was a detour but a useful one. I'm not replacing REST wholesale, but knowing where binary streaming is worth the observability tradeoff is a decision I can now make from actual data rather than instinct. The Splunk structured logging setup is paying off already: two incidents this month that would have been hour-long log digs took about ten minutes with proper correlation IDs.

Home side: got back into the Pi home lab setup after the holiday break. The Pi 3 cluster is still running fine. I've been watching the Pi 4 rumors — some benchmarks look genuinely useful — but nothing announced yet. In the meantime I'm wiring up a Dynatrace trial at work and wondering if I need the same kind of APM discipline at home as at the bank. I probably do, honestly.
