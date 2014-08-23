---
layout: post
title: "Running the ESP8266 standalone without an Arduino"
date: 2014-08-22
author: marcetux
tags: [esp8266, electronics, iot, hardware, maker]
---
The early ESP8266 experiments used an Arduino as the brains and sent AT commands to
the ESP8266 over serial. That's a reasonable starting point, but for a simple
temperature sensor the Arduino is a $25 middleman. The ESP8266 has its own processor;
the question is whether you can program it directly.

The answer is yes, with patience. The module runs at 3.3V, which is not 5V Arduino
logic — you need a level shifter on the TX/RX lines or a 3.3V FTDI cable for
programming. The default firmware is the AT command set, but the chip itself is a
Tensilica Xtensa L106 that accepts custom firmware. Some people in the community have
started writing custom firmware in a Lua interpreter called NodeMCU that's gaining
traction, but the toolchain for flashing is still rough in August.

I got a bare ESP8266-01 module running standalone: breadboard with 3.3V from an LM1117
regulator, RX/TX bridged to an FTDI adapter, GPIO0 pulled low for programming mode.
Flashed the AT firmware at first just to confirm the circuit, then a small custom
hello-world to verify the toolchain. The module posted a temperature reading from a
DS18B20 sensor to a local HTTP endpoint every thirty seconds, drawing about 80mA during
transmission. Too much for a battery sensor but fine for wall-powered. Next step is
deep sleep between readings.
