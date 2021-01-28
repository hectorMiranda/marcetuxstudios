---
layout: post
title: "January notes"
date: 2021-01-27
author: marcetux
tags: [meta, retrospective]
---
First month of the year and the security theme came back hard, which is maybe not
surprising after the way December ended. SolarWinds dominated the first week;
lock files are now enforced in the pipeline; the API gateway token policy is live.
These are the unglamorous fixes that actually move the needle — not new features,
just closing the gap between what the design assumed and what the configuration
actually did.

The .NET 5 migration settled into normalcy. Record types and the better pattern
matching are quiet improvements that I keep reaching for without thinking; C# is
just a nicer language now, incrementally and sustainably. The event-sourcing
evaluation was a good reminder that "could we?" is not the same question as "should
we?" and the answer to the second question requires pricing the operational tail.

Tailwind made a surprise appearance via a team prototype and earned its place through
the constraint-is-the-feature argument rather than aesthetics. The home workbench
picked up again too — dusted off an ESP32 project I stalled on before the holidays
and started looking at LoRa modules, which seem like the right radio technology for
the outdoor sensor mesh I've been planning. Slow start to a year that feels like
it wants to move fast.
