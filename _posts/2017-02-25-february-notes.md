---
layout: post
title: "February notes"
date: 2017-02-25
author: marcetux
tags: [meta, retrospective]
---
February felt productive in a concrete way: code shipped, sensors reporting, APIs
integrated. The ESP32 temperature setup is running in the office and the bedroom now,
both feeding into Home Assistant, and I already caught the office running four degrees
warmer than the thermostat thinks because the sensor is across the room from the
return duct. Useful data.

At SolidCommerce the eBay integration is in final QA. The RabbitMQ pipeline is holding
steady — we processed three times the January volume this month with no queue backup,
which is the whole point of decoupling consumers from intake. The GraphQL proof-of-
concept got five minutes in the team demo and generated more questions than I expected,
mostly positive. I'll write it up more formally when there's time to run it past the
architects.

The TypeScript strict mode cleanup is ongoing — it's a background task that fits in
thirty-minute sessions. I'm treating it like paying off technical debt one bill at a
time: not exciting, but you notice when the balance goes down. Goal for March is to
finish the service layer and start on the component tree.
