---
layout: post
title: "ESP32-S3 for TinyML, a brief setup guide"
date: 2024-08-20
author: marcetux
tags: [esp32, embedded, tinyml, edge-ai, hardware]
---
The ESP32-S3 has a vector extension that makes it noticeably better for small
neural network inference than the standard ESP32. I've been running a keyword
spotter on one for the home lab and the setup is less painful than it was a year
ago. The toolchain is still IDF (ESP-IDF 5.x), TensorFlow Lite Micro is still the
inference framework, and the quantization story is still INT8 because that's what
fits in 512 KB of SRAM.

The practical constraint is memory, not compute. A TFLite Micro model needs to fit
in the IRAM or SRAM the device has after the OS, WiFi stack, and application code.
A keyword spotting model at INT8 quantization sits around 40–80 KB depending on
vocabulary size and model depth; that's fine. Any model larger than about 200 KB
starts competing with the WiFi stack for heap and things start crashing in ways that
are confusing to debug because the failure is at runtime, not at compile time. The
discipline: quantize aggressively, use the MicroInterpreter's static arena allocator
so you control the memory budget explicitly, and measure peak usage before you ship.

What it's good for in a home lab: wake-word detection, simple sensor anomaly
classification, on-device keyword spotting that controls an MQTT-connected system
without sending audio off-device. The inference-at-the-edge value proposition is
privacy: the model runs on the chip, the raw sensor data never leaves it. That's
worth more than it sounds once you're serious about what lives on your home network.
