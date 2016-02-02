---
layout: post
title: "Raspberry Pi 3 landed and the WiFi works"
date: 2016-02-01
author: marcetux
tags: [raspberry-pi, hardware, linux, home]
---
The Pi 3 shipped on its anniversary — 29 February, four years of Raspberry Pi — and I
had one in two days. The headline spec: Cortex-A53 at 1.2 GHz, 1 GB RAM, and finally
802.11n and Bluetooth built in. On paper that's a significant jump from the Pi 2;
on the bench it feels like one too.

The WiFi was the thing I actually needed. I put Raspbian on a card, connected to my
network from the first boot config, and the USB dongle driver roulette I'd been playing
since the Pi 2 was just gone. The onboard chip shows up as a standard interface,
Mosquitto came right up, and every Node service I had on the old box migrated in about
an hour. The Cortex-A53 chews through the broker load without the occasional hiccup the
Pi 2 had under burst traffic.

I retired the old laptop-in-the-closet setup for good this weekend. 5W at the wall
instead of 40W, no fan noise, and one fewer power brick. I'll put the Pi 2 toward
something dedicated — probably a sensor hub for the kitchen sensors — rather than
running two general-purpose services on one board. The Pi 3 is the first single-board
computer I'd actually recommend as a home server without caveats.
