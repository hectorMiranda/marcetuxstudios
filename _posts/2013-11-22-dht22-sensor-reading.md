---
layout: post
title: "Reading the DHT22 sensor from Arduino and why timing matters"
date: 2013-11-22
author: marcetux
tags: [electronics, arduino, hardware, sensors, c]
---
The ambient sensor hub has been collecting data for a few weeks and the DHT22 readings
are mostly good, with occasional garbage values that seemed random until I looked at
the timing. The DHT22's single-wire protocol is notoriously timing-sensitive: the
sensor encodes bits by modulating the duration of a high-voltage pulse, and reading
it requires precise microsecond measurement of those pulse widths on the Arduino.

The stock `DHT.h` library handles this well under normal conditions, but `Serial.println()`
calls inside the main loop were introducing enough jitter in the interrupt timing to
occasionally mis-sample a bit, which produces a reading that fails the checksum and
gets discarded, or worse, passes the checksum with a wrong value. The fix is to take
the DHT reading before any `Serial` output, and to disable interrupts with `noInterrupts()`
for the duration of the bit-reading loop — which is what the library does internally
anyway when I'm not interfering with it from outside.

The larger lesson is that real-time protocol timing on an Arduino is sensitive to
anything that introduces latency: `Serial` output, `delay()` in the wrong place,
interrupt handlers that run too long. The DHT22 is forgiving compared to real
single-wire protocols, but reading it correctly made me understand why the standard
advice is "read the sensor, store the value, print later." Separate the time-sensitive
acquisition from the serial output and the garbage readings disappear.
