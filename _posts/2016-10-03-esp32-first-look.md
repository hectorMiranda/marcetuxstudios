---
layout: post
title: "ESP32 first look and it is not just a faster ESP8266"
date: 2016-10-03
author: marcetux
tags: [esp32, electronics, hardware, home, iot]
---
Espressif shipped the ESP32 modules commercially this month and I got a DevKitC board
from a Chinese distributor the first week of October. The spec sheet is a notable jump
from the ESP8266: dual-core 240 MHz Xtensa LX6, 520 KB SRAM, Bluetooth 4.2 plus WiFi,
more GPIO, hardware SPI and I2C peripherals instead of the bit-banged versions the
ESP8266 forces you to use. On paper it looked like an ESP8266 with more horsepower.
In practice it's a different class of device.

The dual-core is the part that actually changes what you can build. On the ESP8266,
running a WiFi-connected sensor means managing the radio timing carefully in the
application code — the WiFi stack and your application share one core, and if your
code does something slow the radio can starve and disconnect. On the ESP32, the WiFi
stack runs on core 0 and your application runs on core 1. You can write blocking sensor
code without thinking about the radio. The hardware PWM and DAC channels are additional
things the ESP8266 made you work around; the ESP32 just has them.

The toolchain is still early — the Arduino ESP32 core isn't merged yet and I'm using
the Espressif IDF directly, which is more friction than the Arduino workflow. But for a
board this capable, learning the IDF is probably the right investment. The ESP8266 was
the right tool for simple sensors; the ESP32 is the right tool for anything that needed
more and was making do.
