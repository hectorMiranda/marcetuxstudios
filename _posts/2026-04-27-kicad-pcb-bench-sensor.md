---
layout: post
title: "KiCad board for the studio bench sensor"
date: 2026-04-27
author: marcetux
tags: [electronics, kicad, pcb, esp32, home-lab]
---
The studio has an annoying problem: the temperature gradient between the soldering bench and the rack corner is large enough that the rack fans run hotter than they should because the ambient sensor is reading bench air. A simple fix would be a USB thermometer pointed at the rack; the fun fix is a small custom board with an ESP32-C3 and a couple of sensors that publishes to the MQTT broker and feeds into the observability stack I have running in the cluster.

I hadn't done a KiCad layout from scratch since last year, and I was pleased to find that the Symbol Editor workflow has gotten cleaner. The board itself is simple — ESP32-C3 module footprint, a BME280 on the I2C bus for temperature/humidity/pressure, a passive infrared sensor on a GPIO for motion detection (so the dashboard knows when the space is occupied), decoupling caps, and a USB-C power input with proper ESD protection. Two layers, nothing tricky. The autorouter gets me eighty percent of the way; I hand-route the I2C traces and the USB differential pair.

The thing I appreciate about small boards like this is that they're fast enough to be satisfying — design Friday evening, check Gerbers Saturday, order by weekend. The fabrication lead time is the long pole, not the design. The boards will arrive in two or three weeks, and the firmware is basically done because it's the same MQTT-publishing pattern I've been using for two years. The delta from the last board is the new footprint, the new sensor library, and the USB-C power path. Most of the work is already done; I just need the copper.
