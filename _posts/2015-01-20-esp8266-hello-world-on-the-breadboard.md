---
layout: post
title: "ESP8266 hello world on the breadboard"
date: 2015-01-20
author: marcetux
tags: [esp8266, electronics, wifi, mqtt, hardware]
---
The ESP8266 modules I ordered in December finally arrived — $3 WiFi+microcontroller
in a package smaller than my thumb, and the specs seemed too good to believe until I got
one on the breadboard. Monday night I had it joining my home network, responding to
AT commands over serial, and publishing a temperature reading to an MQTT broker running
on the Pi 2.

The first session was more hardware wrangling than software. The module runs at 3.3V and
most USB-UART adapters are 5V — level shifting is not optional unless you enjoy frying
$3 chips. Pulled CH_PD high to enable the chip, tied GPIO0 high for normal operation
(low for flash mode), powered it from the 3.3V rail of the adapter, and the AT firmware
responded. From there the MQTT library for the ESP8266 Arduino core was the path of
least resistance: a few calls to connect, subscribe, and publish, flash it, done.

The thing that keeps striking me is the economics. A Raspberry Pi is a serious computer;
an ESP8266 is a throw-it-in-a-wall-junction-box sensor node that costs nothing and runs
for months on a small battery. The two aren't competing — they're a natural stack.
Pi 2 as the hub, a handful of ESP8266s scattered around as dumb sensor endpoints, MQTT
as the glue. This is the home-automation architecture I've been slowly building toward.
