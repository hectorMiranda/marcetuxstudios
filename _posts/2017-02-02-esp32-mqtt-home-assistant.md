---
layout: post
title: "ESP32 sensors reporting into Home Assistant over MQTT"
date: 2017-02-02
author: marcetux
tags: [esp32, mqtt, homeassistant, iot, electronics]
---
The ESP32 batch I ordered in January finally had a reason to get out of the static bag.
The plan was simple: read temperature and humidity from a DHT22, publish the readings
every 30 seconds to an MQTT topic, and have Home Assistant pick them up. The ESP32 runs
the Arduino framework, which I'm comfortable with from ESP8266 days, and the Wi-Fi
library is much more stable than what the 8266 shipped with at the start.

The firmware is short — under 100 lines. Connect Wi-Fi, connect the MQTT broker on the
Pi, loop: read sensor, publish to `home/sensors/office/temp` and
`home/sensors/office/humidity`, sleep 30 seconds. Home Assistant's MQTT integration
uses a `sensor:` block with the topic and a value template; once it sees the first
retained message it shows up in the UI. The autodiscovery feature is even cleaner if
you publish to the right topic scheme — HA picks up the entity without a YAML entry.

The one thing I'd do differently: power the board over a proper USB supply, not a
random phone charger. The DHT22 is sensitive enough that voltage sag from a cheap
charger skews humidity readings high. I measured the difference with a lab supply and a
wall wart — almost six points of humidity. Boring problem, boring fix: use a good
supply and it goes away. Board and firmware are in examples.
