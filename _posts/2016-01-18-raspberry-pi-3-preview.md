---
layout: post
title: "Waiting on the Pi 3 and moving the home server off an old laptop"
date: 2016-01-18
author: marcetux
tags: [raspberry-pi, hardware, linux, home]
---
The home server I use for MQTT brokering and a few small services has been an old laptop
in the closet for two years — loud fan, 40W at idle, running Ubuntu. It does the job but
it's ridiculous for what amounts to a glorified message bus and a couple of Node scripts.
A Pi 2 has been on my radar for months, and with rumors of a Pi 3 with onboard WiFi I
kept putting it off.

The rumors appear to be credible — trade press has been dropping hints about a Pi 3 with
a 64-bit ARM chip and integrated 802.11n. If that lands in the next couple of months the
right move is to wait. The thing I actually care about is the WiFi: the Pi 2 needs a USB
dongle that eats one of the four USB ports and introduces a driver roulette every time
there's a kernel update. Built-in wireless removes an entire category of annoyance.

For now I migrated the MQTT broker — Mosquitto — to the Pi 2 that was running
the home-automation sensor dashboard. Split the roles, two smaller boards instead of
one laptop. The noise dropped to zero and the idle draw is probably 5W total. Once the
Pi 3 is real I'll consolidate again and retire the Pi 2 to a different experiment. In
the meantime: quieter closet, same functionality, one more drawer for the laptop parts.
