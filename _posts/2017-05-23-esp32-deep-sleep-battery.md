---
layout: post
title: "ESP32 deep sleep for battery-powered sensors"
date: 2017-05-23
author: marcetux
tags: [esp32, electronics, iot, home]
---
The office and bedroom sensors from February are USB-powered, which is fine because
they're near outlets. A new sensor for the garage — where I want temperature and door
state — doesn't have a convenient outlet, so I'm running it on a 3.7V LiPo and deep
sleep mode. The difference between the two power strategies is the difference between
a sensor that runs for years on a battery and one that runs for hours.

Deep sleep on the ESP32 shuts down the processor, Wi-Fi, and Bluetooth, leaving only
the RTC (real-time clock) domain running. The board draws about 10 microamps in deep
sleep versus 160-240 milliamps active. If the sensor wakes up every 5 minutes, takes
a reading, connects to Wi-Fi, publishes to MQTT, and goes back to sleep — the active
period is about 3-4 seconds, which means the average current draw is tiny. A 2000mAh
cell should run it for months.

The practical wrinkle: Wi-Fi reconnect time dominates the active period. Each wake-up,
the ESP32 has to re-associate with the AP, get a DHCP address, and reconnect to the
MQTT broker — that's most of the 3-4 seconds. A static IP and storing the BSSID and
channel in RTC memory (which survives deep sleep) cuts the reconnect time roughly in
half. It's in the examples. The garage sensor has been on its first LiPo charge for
three weeks; I'll report the longevity when it dies.
