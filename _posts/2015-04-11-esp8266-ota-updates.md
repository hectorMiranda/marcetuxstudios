---
layout: post
title: "OTA firmware updates on ESP8266 nodes"
date: 2015-04-11
author: marcetux
tags: [esp8266, electronics, arduino, ota, hardware]
---
The temperature node in the bathroom is wired into a junction box and is not something
I want to physically pull to flash new firmware. ESP8266 OTA — over-the-air updates —
solves this cleanly, and the Arduino core's `ArduinoOTA` library makes it simpler than
I expected for a first try.

The setup: include `ArduinoOTA.h`, call `ArduinoOTA.begin()` in `setup()`, call
`ArduinoOTA.handle()` in `loop()`. The board advertises itself via mDNS, and from the
Arduino IDE or `esptool.py` you can push new firmware over the network. The OTA library
handles downloading the image, verifying it, and flashing the inactive partition while
the active code keeps running — the board reboots into the new firmware only if the
transfer succeeds. A failed transfer leaves the old firmware intact.

The thing to be careful about: `ArduinoOTA.handle()` in `loop()` means you can't block
`loop()` for more than a second or so, or the OTA process times out during a transfer.
My nodes had 30-second `delay()` calls for the publish interval, which blocked OTA
entirely. Replacing the `delay` with a non-blocking timer pattern — check elapsed time,
only publish when the interval expires, keep `loop()` fast — fixed it. Knowing your
loop can block is general wisdom for embedded work; OTA just makes the consequence of
forgetting immediately obvious.
