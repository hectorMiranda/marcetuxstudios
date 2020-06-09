---
layout: post
title: "Tracing the ghost load on the energy dashboard"
date: 2020-06-08
author: marcetux
tags: [homelab, iot, electronics, grafana]
---
The mystery baseline on the Grafana energy dashboard — 40 watts that I couldn't
account for — has been bothering me since March. This weekend I traced it. The method
is old-fashioned: turn off circuit breakers one at a time and watch which one makes
the number drop. Fourth breaker: living room, which should be negligible at 2 AM
when nothing's on. It dropped to zero.

The culprit was an old gaming console in standby mode, plugged into a power strip
with two other devices in their own "standby" states. Together they were drawing
40 watts continuously, around the clock, doing nothing. Not a malicious power draw —
just three devices that never fully turn off. The console alone was 22 watts in
standby, which over a month is about 16 kWh, which is money but more annoyingly is
heat I'm paying for in a room where I'm also paying the AC.

The fix is a smart plug that cuts power to the strip on a schedule — on at 6 PM, off
at midnight. The ESP32 on the home network already runs MQTT; adding a Tasmota-flashed
Sonoff plug to the broker was fifteen minutes. The daily load on that circuit is now
the actual usage pattern. The dashboard went from "here is data I don't fully
understand" to "here is data I can act on." That was the point all along.
