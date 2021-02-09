---
layout: post
title: "LoRa range test on the bench"
date: 2021-02-08
author: marcetux
tags: [electronics, esp32, lora, hardware, iot]
---
Picked up a pair of RA-02 LoRa modules — SX1278 chipset, 433 MHz — and spent a
Saturday afternoon wiring them to ESP32 devboards and seeing how far a small packet
travels. Short answer: much farther than I expected for hardware that costs less
than a lunch. Long answer: it depends heavily on antenna, line-of-sight, and the
spreading factor you choose.

The spreading factor trade-off is the interesting part. LoRa isn't a fixed data
rate; you dial a spreading factor from SF7 to SF12. Higher spreading factor means
more redundancy per bit, longer range, and dramatically lower throughput. SF12 can
get a packet across a kilometer of suburban clutter; it also means a 20-byte payload
takes several seconds to transmit and the radio is on the whole time, eating
battery. SF7 is faster and shorter. For a soil sensor that reports every ten
minutes, SF10 or SF11 is the right call. For a node that's plugged in and you want
responsiveness, SF7 is fine.

Bench result with a simple whip antenna: SF10, 433 MHz, I was getting solid
reception at 200 m line-of-sight in the parking structure, which is more than
enough for the outdoor mesh I'm planning. Next step is writing a proper packet
format — right now it's just a raw float — and adding a small OLED to the receiver
node so I can see received signal strength without a laptop attached.
