---
layout: post
title: "Revising the LoRa mesh firmware for better duty cycling"
date: 2022-01-14
author: marcetux
tags: [electronics, lora, embedded, microcontroller]
---
The LoRa mesh I've been running around the house since last year was wasting battery.
Nodes were staying in receive mode continuously between transmissions, which is fine
on USB power but kills a coin cell in a day. I pulled the firmware this weekend and
added proper sleep-wake cycles, and the difference is significant enough that I'm
annoyed I didn't do it sooner.

The pattern with LoRa is **listen windows** — the node powers the radio up for a brief
window, checks for any incoming message, then sleeps the MCU and the radio module for
the bulk of the interval. For sensor nodes that only need to report every few minutes,
the radio is on less than one percent of the time. On my ESP32 nodes with the SX1276
module, the combination of MCU deep sleep and putting the radio into sleep mode between
windows dropped the average current from around forty milliamps to under a milliamp in
the idle state.

The tricky part is coordinating windows in a mesh without a shared clock — nodes drift.
I added a small jitter on the transmit interval so they don't collide, and receivers
keep their window open a bit longer than the nominal interval to account for drift. It's
not elegant, but it works for the scale I'm running. The mesh is now a set of nodes that
mostly sleep and occasionally gossip. Quiet at the boundary; right where I want it.
