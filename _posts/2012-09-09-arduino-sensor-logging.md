---
layout: post
title: "Logging sensors with an Arduino over the weekend"
date: 2012-09-09
author: marcetux
tags: [arduino, electronics, sensors, hardware]
---

Follow-up to yesterday's Pi: while that's still downloading packages, I wired a
DHT11 temperature/humidity sensor to an Arduino Uno to start collecting data.

The Arduino sketch is almost insultingly simple — read the sensor every few
seconds, print `temp,humidity` over the serial port. The interesting design
decision is *who keeps the data*. The Uno has no clock and no storage worth
mentioning, so it shouldn't try. It just emits readings; something smarter
timestamps and stores them.

So the division of labor is shaping up: **Arduino senses, Pi remembers.** The Uno
streams comma-separated readings over USB serial, and a small Python script on the
Pi reads the line, stamps it with the current time, and appends to a file. Later
I'll put a real datastore behind it, but a flat file is honestly fine for a
bedroom-temperature graph.

There's a lesson in here that applies at work too: push state to the layer that's
actually good at keeping it, and let the cheap nodes stay dumb and fast.

*Update: the sketch and the Pi-side logger now live in `examples/2012/arduino/`
(`dht11_logger.ino` and `logger.py`).*
