---
layout: post
title: "ESP32 deep sleep and wakeup sources"
date: 2016-11-09
author: marcetux
tags: [esp32, electronics, hardware, home, iot]
---
Getting the ESP32 into deep sleep with a timer wakeup took longer than it should have
because the IDF docs assume you know a few things about the chip's power architecture
that aren't obvious from the ESP8266. The ESP32 has a ULP (Ultra Low Power) coprocessor
that can stay on during deep sleep and sample GPIOs; the main cores and most peripherals
power down completely. The sleep current with ULP disabled is around 10 µA, similar to
the ESP8266 in deep sleep.

The wakeup configuration is the part that surprised me. On the ESP8266, wakeup from
deep sleep requires grounding RST from GPIO16 — a hardware requirement that determines
your PCB layout. On the ESP32, wakeup sources are configurable in software: timer
wakeup, GPIO wakeup, touch wakeup, ULP wakeup. `esp_sleep_enable_timer_wakeup(
MICROSECONDS)` followed by `esp_deep_sleep_start()` is the complete software side. No
hardware wiring required for timer wakeup.

The practical result for the sensor board I'm designing: the ESP32 wakes every 10
minutes, reads the BME280 over I2C, publishes to MQTT, and sleeps. The I2C peripheral
comes back up in a couple of hundred milliseconds after wakeup; the WiFi stack takes
longer — 800 ms to 1.5 seconds depending on AP response time. Battery life on a 1000
mAh LiPo at 10-minute intervals is measured in months rather than weeks. The flexible
wakeup story is one more reason the ESP32 is the right upgrade from the ESP8266.
