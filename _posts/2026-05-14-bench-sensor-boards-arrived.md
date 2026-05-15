---
layout: post
title: "The bench sensor boards arrived"
date: 2026-05-14
author: marcetux
tags: [electronics, pcb, esp32, home-lab, kicad]
---
The boards from the April KiCad session showed up last week, and I'm pleased to report zero backwards footprints and only one self-inflicted problem: I put the test pads on the wrong side of the BME280, which means you can't probe them without flipping the board over. Not a functional issue, just inconvenient, and noted for the next revision. Everything else assembled cleanly in about forty minutes including the USB-C connector, which I was prepared to swear at for longer than that.

Firmware was done before the boards arrived, so the bring-up was mostly plugging it in, watching the MQTT broker receive the first temperature/humidity/pressure reading, and then spending an hour tuning the Grafana panel to display the three sensors (bench, rack front, rack rear) on the same graph with the right scales. The motion detection worked on the first try, which is suspicious — usually something is backwards — so I'm watching it carefully. The studio is "occupied" correctly based on the PIR, which means the automations that dim the bench lamp when I leave actually work now.

What I want to do next is add a CO₂ sensor, because the ventilation circuit in the studio is good but not great for long solder sessions, and having a number to look at is better than guessing. The BME280 gives me air quality proxies but not CO₂ directly. A second sensor module on the same I2C bus is a one-day project if I can find a footprint I trust. The board has two unpopulated I2C headers that I put there exactly for this reason.
