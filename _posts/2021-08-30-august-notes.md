---
layout: post
title: "August notes"
date: 2021-08-30
author: marcetux
tags: [meta, retrospective]
---
August was a month where a lot of the interesting intellectual energy was outside
the day job. The smart contract reading weekend opened something up — not a
certainty, more a growing suspicion that the programmable-contract and provenance
primitives in this space have a real engineering future that isn't obvious from
the NFT-price-line discourse. I've been going back through Solidity documentation
in the evenings, slowly, without an application in mind yet.

The day job delivered two solid lessons: backpressure is not a database problem,
it's a design problem you have to build into the consumer explicitly; and schema
migrations require a different mental model than application code changes. Both
were hard-earned. The zero-downtime migration one especially — we'd been getting
away with fast migrations on small tables and finally hit a table large enough for
the locking math to matter.

Home workbench: the third LoRa board is assembled and running. The outdoor sensor
mesh is now three nodes: one on the roof feeding temperature and humidity, one in
the garage, one inside as the gateway. Data flows into a local InfluxDB instance
and a Grafana panel I check without any particular purpose. It just satisfies
something to have numbers flowing. The Altium DFM discipline from June is already
paying off — no silk problem this run, and the boards came back cleaner than the
previous rev.
